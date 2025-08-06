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
    /// Provides functionality for generating text content using the Gemini API.
    /// </summary>
    public class TextClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public TextClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates text content based on a user prompt.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for content generation.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateTextAsync(string? model, string prompt)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model

            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateTextAsync.");
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
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
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