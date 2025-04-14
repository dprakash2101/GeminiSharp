using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Response;
using Serilog; // Only Serilog for logging

namespace GeminiSharp.API
{
    /// <summary>
    /// A reusable client for interacting with the Google Gemini API.
    /// </summary>
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class with minimal setup.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional, defaults to Google's endpoint).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiKey"/> is null or empty.</exception>
        public GeminiApiClient(string apiKey, string? baseUrl = null)
            : this(new HttpClient(), apiKey, baseUrl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class with custom HttpClient.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance for making requests.</param>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional, defaults to Google's endpoint).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="apiKey"/> is null.</exception>
        public GeminiApiClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = string.IsNullOrWhiteSpace(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            _baseUrl = baseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
        }

        /// <summary>
        /// Sends a request to the Gemini API for content generation (text or structured).
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload.</param>
        /// <param name="endpoint">The API endpoint (e.g., "generateContent").</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="GenerateContentResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> or <paramref name="endpoint"/> is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response or other errors occur.</exception>
        public async Task<GenerateContentResponse> SendRequestAsync<TRequest>(string model, TRequest request, string endpoint = "generateContent")
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                Log.Error("Model name is empty or null.");
                throw new ArgumentException("Model cannot be empty", nameof(model));
            }

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                Log.Error("Endpoint is empty or null.");
                throw new ArgumentException("Endpoint cannot be empty", nameof(endpoint));
            }

            string url = $"{_baseUrl}{model}:{endpoint}?key={_apiKey}";

            try
            {
                // Log request details
                string requestContent = JsonSerializer.Serialize(request);
                Log.Information("Sending request to {Url} with body: {RequestContent}", url, requestContent);

                var response = await _httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(
                        errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Log error details
                    Log.Error("API request to {Endpoint} failed with status code {StatusCode}. Error: {ErrorContent}",
                        endpoint, response.StatusCode, errorContent);

                    throw new GeminiApiException(
                        errorResponse?.Error?.Message ?? "Unknown error occurred",
                        response.StatusCode,
                        errorResponse
                    );
                }

                var responseContent = await response.Content.ReadFromJsonAsync<GenerateContentResponse>()
                    ?? throw new InvalidOperationException("Failed to deserialize response from Gemini API.");

                // Log success
                Log.Information("Successfully received response for model {Model} at endpoint {Endpoint}.", model, endpoint);

                return responseContent;
            }
            catch (Exception ex)
            {
                // Log exception details
                Log.Error(ex, "Error while sending request to {Endpoint} for model {Model}.", endpoint, model);
                throw;
            }
        }
    }
}