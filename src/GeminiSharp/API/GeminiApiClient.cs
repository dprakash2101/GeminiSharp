using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Utilities;
using Serilog;
using System.Net.Http;

namespace GeminiSharp.API
{
    /// <summary>
    /// A reusable client for interacting with the Google Gemini API.
    /// This class handles the low-level HTTP communication, authentication, and retry logic.
    /// </summary>
    internal class GeminiApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly RetryConfig _retryConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="httpClientFactory">The HTTP client factory used to create <see cref="HttpClient"/> instances.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional, defaults to Google's endpoint).</param>
        /// <param name="retryConfig">Retry configuration (optional, defaults to 3 retries for 429, 500, 503).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiKey"/> is null or empty.</exception>
        public GeminiApiClient(string apiKey, IHttpClientFactory httpClientFactory, string? baseUrl = null, RetryConfig? retryConfig = null)
        {
            _apiKey = string.IsNullOrWhiteSpace(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            _httpClientFactory = httpClientFactory;
            _baseUrl = baseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
            _retryConfig = retryConfig ?? new RetryConfig();
        }

        /// <summary>
        /// Sends a request to the Gemini API for various operations (e.g., content generation, embeddings, token counting).
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <typeparam name="TResponse">The expected type of the response.</typeparam>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash", "embedding-001").</param>
        /// <param name="request">The request payload.</param>
        /// <param name="endpoint">The API endpoint (e.g., "generateContent", "embedContent", "countTokens").</param>
        /// <returns>A task representing the asynchronous operation, returning a <typeparamref name="TResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response after retries.</exception>
        /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(string model, TRequest request, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                Log.Error("Model name is empty or null.");
                throw new ArgumentException("Model cannot be empty", nameof(model));
            }

            string url = $"{_baseUrl}{model}:{endpoint}";
            string requestContent = JsonSerializer.Serialize(request);

            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                Log.Information("Sending request to {Url} with body: {RequestContent}", url, requestContent);

                var response = await httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(
                        errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    Log.Error("API request failed with status code {StatusCode}. Error: {ErrorContent}",
                        response.StatusCode, errorContent);

                    throw new GeminiApiException(
                        errorResponse?.Error?.Message ?? "Unknown error occurred",
                        response.StatusCode,
                        errorResponse
                    );
                }

                var responseContent = await response.Content.ReadFromJsonAsync<TResponse>()
                    ?? throw new InvalidOperationException($"Failed to deserialize response from Gemini API to {typeof(TResponse).Name}.");

                Log.Information("Successfully received response for model {Model}.", model);

                return responseContent;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while sending request for model {Model}.", model);
                throw;
            }
        }

        /// <summary>
        /// Sends a GET request to the Gemini API to retrieve information (e.g., model details).
        /// </summary>
        /// <typeparam name="TResponse">The expected type of the response.</typeparam>
        /// <param name="model">The Gemini model to query (e.g., "gemini-1.5-flash").</param>
        /// <returns>A task representing the asynchronous operation, returning a <typeparamref name="TResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
        public async Task<TResponse> SendGetRequestAsync<TResponse>(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                Log.Error("Model name is empty or null.");
                throw new ArgumentException("Model cannot be empty", nameof(model));
            }

            string url = $"{_baseUrl}{model}";
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                Log.Information("Sending GET request to {Url}", url);

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(
                        errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    Log.Error("API request failed with status code {StatusCode}. Error: {ErrorContent}",
                        response.StatusCode, errorContent);

                    throw new GeminiApiException(
                        errorResponse?.Error?.Message ?? "Unknown error occurred",
                        response.StatusCode,
                        errorResponse
                    );
                }

                var responseContent = await response.Content.ReadFromJsonAsync<TResponse>()
                    ?? throw new InvalidOperationException($"Failed to deserialize response from Gemini API to {typeof(TResponse).Name}.");

                Log.Information("Successfully received response for model {Model}.", model);

                return responseContent;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while sending GET request for model {Model}.", model);
                throw;
            }
        }
    }
}