using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

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
        /// <param name="httpClient">The HTTP client used for API requests.</param>
        /// <param name="apiKey">The API key for authenticating requests.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        public GeminiStructuredClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(httpClient, apiKey, baseUrl);
        }

        /// <summary>
        /// Generates structured content based on user input and a predefined JSON schema.
        /// The user is responsible for defining the schema in their request.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <returns>The structured output as a <see cref="GenerateContentResponse"/>.</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown if an unexpected error occurs.</exception>
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
                    ResponseMimeType = "application/json",
                    ResponseSchema = jsonSchema
                }
            };

            try
            {
                return await _apiClient.GenerateStructuredContentAsync(model, request);
            }
            catch (GeminiApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating structured content.", ex);
            }
        }
    }
}
