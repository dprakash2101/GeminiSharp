using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Utilities;
using Serilog;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides an interface for generating structured output using the Gemini API.
    /// </summary>
    public class GeminiStructuredClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiStructuredClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        /// <param name="retryConfig">Retry configuration (optional).</param>
        public GeminiStructuredClient(string apiKey, string? baseUrl = null, RetryConfig? retryConfig = null)
        {
            // Pass the retryConfig along with other parameters to GeminiApiClient
            _apiClient = new GeminiApiClient(apiKey, httpClient: null, baseUrl: baseUrl, retryConfig: retryConfig);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiStructuredClient"/> class with a custom HttpClient.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests.</param>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        /// <param name="retryConfig">Retry configuration (optional).</param>
        public GeminiStructuredClient(HttpClient httpClient, string apiKey, string? baseUrl = null, RetryConfig? retryConfig = null)
        {
            // Pass the custom HttpClient, apiKey, baseUrl, and retryConfig to GeminiApiClient
            _apiClient = new GeminiApiClient(apiKey, httpClient, baseUrl, retryConfig);
        }

        /// <summary>
        /// Generates structured content based on a prompt and a JSON schema.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the structured output.</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="ArgumentException">Thrown if the prompt or schema is empty or null.</exception>
        public async Task<GenerateContentResponse?> GenerateStructuredContentAsync(string model, string prompt, object jsonSchema)
        {
      
            var request = new GeminiStructuredRequest
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
                },
                GenerationConfig= new GenerationConfig
                {
                    response_mime_type = "application/json",
                    response_schema = jsonSchema
                }
            };

            try
            {
                Log.Information("Generating structured content for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync(model, request);
                Log.Information("Successfully generated structured content for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating structured content for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating structured content for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating structured content.", ex);
            }
        }
    }
}