using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides an interface for generating structured output using the Gemini API.
    /// </summary>
    public class GeminiStructuredClient
    {
        private readonly IGeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiStructuredClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client.</param>
        public GeminiStructuredClient(IGeminiApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Generates structured content based on a prompt and a JSON schema.
        /// </summary>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the structured output.</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="ArgumentException">Thrown if the prompt or schema is empty or null.</exception>
        public async Task<GenerateContentResponse?> GenerateStructuredContentAsync(string prompt, object jsonSchema, CancellationToken cancellationToken = default)
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
                GenerationConfig = new GenerationConfig
                {
                    response_mime_type = "application/json",
                    response_schema = jsonSchema
                }
            };

            try
            {
                Log.Information("Generating structured content with prompt: {Prompt}", prompt);
                var response = await _apiClient.GenerateContentAsync<GeminiStructuredRequest, GenerateContentResponse>(request, cancellationToken);
                Log.Information("Successfully generated structured content.");
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating structured content.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating structured content.");
                throw new Exception("An unexpected error occurred while generating structured content.", ex);
            }
        }
    }
}
