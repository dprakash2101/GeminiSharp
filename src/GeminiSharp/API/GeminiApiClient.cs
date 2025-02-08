using System.Text;
using System.Text.Json;
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
        /// Sends a request to the Gemini API.
        /// </summary>
        public async Task<GenerateContentResponse?> GenerateContentAsync(string model, GenerateContentRequest request)
        {
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty", nameof(model));

            string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";

            var jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(url, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API call failed: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GenerateContentResponse>(jsonResponse);
        }
    }
}
