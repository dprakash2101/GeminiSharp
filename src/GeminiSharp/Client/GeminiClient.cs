using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides a high-level interface for interacting with the Gemini API.
    /// </summary>
    public class GeminiClient
    {
        private readonly GeminiApiClient _apiClient;

        public GeminiClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(httpClient, apiKey, baseUrl);
        }

        /// <summary>
        /// Generates content based on user input and returns the full response.
        /// </summary>
        public async Task<GenerateContentResponse?> GenerateContentAsync(string model, string prompt)
        {
            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart>
                        {
                            new RequestContentPart { Text = prompt }
                        }
                    }
                }
            };

            return await _apiClient.GenerateContentAsync(model, request);
        }
    }
}
