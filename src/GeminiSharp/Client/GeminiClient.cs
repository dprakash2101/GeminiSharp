using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides a high-level interface for generating text content with the Gemini API.
    /// </summary>
    public class GeminiClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        public GeminiClient(string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(apiKey, baseUrl);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class with a custom HttpClient.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests.</param>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        public GeminiClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(httpClient, apiKey, baseUrl);
        }

        /// <summary>
        /// Generates text content based on a user prompt.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for content generation.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        public async Task<GenerateContentResponse> GenerateContentAsync(string model, string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }

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

            try
            {
                Log.Information("Generating content for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync(model, request, "generateContent");
                Log.Information("Successfully generated content for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content.", ex);
            }
        }
    }
}