using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeminiSharp.Client.Clients
{
    /// <summary>
    /// Provides functionality for generating structured content using the Gemini API.
    /// </summary>
    public class StructuredContentClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredContentClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public StructuredContentClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates structured content based on a prompt and a JSON schema.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <param name="generationConfig">Optional. Configuration for the generation process, including response MIME type and schema.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the structured output.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the JSON schema is null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateStructuredContentAsync(string? model, string prompt, object jsonSchema, GenerationConfig? generationConfig = null)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
                if (string.IsNullOrWhiteSpace(prompt))
                {
                    Log.Error("Prompt is empty or null in GenerateStructuredContentAsync.");
                    throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
                }
                if (jsonSchema == null)
                {
                    Log.Error("JSON schema is null in GenerateStructuredContentAsync.");
                    throw new ArgumentNullException(nameof(jsonSchema));
                }

                var effectiveGenerationConfig = generationConfig ?? new GenerationConfig();
                effectiveGenerationConfig.response_mime_type = effectiveGenerationConfig.response_mime_type ?? "application/json";
                effectiveGenerationConfig.response_schema = effectiveGenerationConfig.response_schema ?? jsonSchema;

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
                },
                    GenerationConfig = effectiveGenerationConfig
                };

                try
                {
                    Log.Information("Generating structured content for model {Model} with prompt: {Prompt}", model, prompt);
                    var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
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
}
