using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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

        public GeminiApiClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _baseUrl = baseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
        }

        /// <summary>
        /// Calls the Gemini API to generate content.
        /// Throws a GeminiApiException if the API returns an error response.
        /// </summary>
        public async Task<GenerateContentResponse> GenerateContentAsync(string model, GenerateContentRequest request)
        {
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty", nameof(model));

            string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";

            var jsonRequest = JsonSerializer.Serialize(request);
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
