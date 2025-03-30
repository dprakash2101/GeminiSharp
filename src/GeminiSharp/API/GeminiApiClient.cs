using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Configuration;
using GeminiSharp.Models;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Options;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

namespace GeminiSharp.API
{
    /// <summary>
    /// Provides low-level access to the Google Gemini API for generating content and structured responses.
    /// </summary>
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly RetryExecutor _retryExecutor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class with configuration options.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for making API requests.</param>
        /// <param name="options">The configuration options including API key, base URL, and retry settings.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="options"/> is null, or when required options properties are null.</exception>
        public GeminiApiClient(HttpClient httpClient, GeminiClientOptions options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = options.ApiKey ?? throw new ArgumentNullException(nameof(options.ApiKey));
            _baseUrl = options.BaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
            _retryExecutor = new RetryExecutor(options.MaxRetries, options.BackoffSeconds);
        }

        /// <summary>
        /// Generates content asynchronously using the specified Gemini model and request payload.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload containing the prompt and generation settings.</param>
        /// <returns>A task representing the generated content response.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is null or whitespace.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response or retries are exhausted.</exception>
        /// <exception cref="Exception">Thrown when deserialization of the response fails.</exception>
        public async Task<GenerateContentResponse> GenerateContentAsync(string model, GenerateContentRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));

            return await _retryExecutor.ExecuteWithRetries(async () =>
            {
                string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";
                var response = await _httpClient.PostAsJsonAsync(url, request);
                return await HandleResponse(response);
            });
        }

        /// <summary>
        /// Generates structured content asynchronously using the specified Gemini model and structured request payload.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload containing the prompt and structured output settings.</param>
        /// <returns>A task representing the structured content response.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is null or whitespace.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response or retries are exhausted.</exception>
        /// <exception cref="Exception">Thrown when deserialization of the response fails.</exception>
        public async Task<GenerateContentResponse> GenerateStructuredContentAsync(string model, GeminiStructuredRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));

            return await _retryExecutor.ExecuteWithRetries(async () =>
            {
                string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";
                var response = await _httpClient.PostAsJsonAsync(url, request);
                return await HandleResponse(response);
            });
        }

        /// <summary>
        /// Handles the HTTP response from the Gemini API, deserializing it into a content response or throwing an exception on error.
        /// </summary>
        /// <param name="response">The HTTP response message to process.</param>
        /// <returns>The deserialized <see cref="GenerateContentResponse"/>.</returns>
        /// <exception cref="GeminiApiException">Thrown when the response indicates an API error.</exception>
        /// <exception cref="Exception">Thrown when deserialization fails.</exception>
        private async Task<GenerateContentResponse> HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                throw new GeminiApiException(
                    errorResponse?.Error?.Message ?? "Unknown error occurred",
                    response.StatusCode,
                    errorResponse
                );
            }

            return await response.Content.ReadFromJsonAsync<GenerateContentResponse>()
                   ?? throw new Exception("Failed to deserialize response from Gemini API.");
        }
    }
}