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
    /// Provides a high-level interface for generating text content with the Gemini API.
    /// </summary>
    public class GeminiClient
    {
        private readonly IGeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client.</param>
        public GeminiClient(IGeminiApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Generates text content based on a user prompt.
        /// </summary>
        /// <param name="prompt">The input prompt for content generation.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        public async Task<GenerateContentResponse> GenerateContentAsync(string prompt, CancellationToken cancellationToken = default)
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
                Log.Information("Generating content with prompt: {Prompt}", prompt);
                var response = await _apiClient.GenerateContentAsync<GenerateContentRequest, GenerateContentResponse>(request, cancellationToken);
                Log.Information("Successfully generated content.");
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content.");
                throw new Exception("An unexpected error occurred while generating content.", ex);
            }
        }
    }
}
