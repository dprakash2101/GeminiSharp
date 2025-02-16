using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

namespace GeminiSharp.API
{
    /// <summary>
    /// Handles direct communication with the Google Gemini API.
    /// </summary>
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance used for making requests.</param>
        /// <param name="apiKey">The API key for authenticating requests.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional, defaults to Google's endpoint).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="apiKey"/> is null.</exception>
        public GeminiApiClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _baseUrl = baseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
        }

        /// <summary>
        /// Calls the Gemini API to generate content using text-based input.
        /// Throws a <see cref="GeminiApiException"/> if the API returns an error response.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload containing the input prompt and generation settings.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="GenerateContentResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the provided model name is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown when deserialization of the API response fails.</exception>
        public async Task<GenerateContentResponse> GenerateContentAsync(string model, GenerateContentRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));

            string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, request);

            if (!response.IsSuccessStatusCode)
            {
                // Read the error response
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Throw API Exception with the error details
                throw new GeminiApiException(
                    errorResponse?.Error?.Message ?? "Unknown error occurred",
                    response.StatusCode,
                    errorResponse
                );
            }

            return await response.Content.ReadFromJsonAsync<GenerateContentResponse>()
                   ?? throw new Exception("Failed to deserialize response from Gemini API.");
        }

        /// <summary>
        /// Calls the Gemini API to generate structured content using a user-defined schema.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload containing the input prompt and structured output settings.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="GenerateContentResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the provided model name is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown when deserialization of the API response fails.</exception>
        public async Task<GenerateContentResponse> GenerateStructuredContentAsync(string model, GeminiStructuredRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));

            string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, request);

            if (!response.IsSuccessStatusCode)
            {
                // Read the error response
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Throw API Exception with the error details
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
