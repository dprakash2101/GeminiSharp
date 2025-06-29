using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides an interface for generating images using the Gemini API.
    /// </summary>
    public class GeminiImageGenerationClient
    {
        private readonly IGeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiImageGenerationClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client.</param>
        public GeminiImageGenerationClient(IGeminiApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Generates an image from a text prompt using the Gemini image model.
        /// </summary>
        /// <param name="prompt">The image generation prompt.</param>
        /// <param name="config">The configuration for image generation.</param>
        /// <param name="includeText">Whether to include a text response along with the image (default is false).</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the image (and optional text).</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the config is null.</exception>
        public async Task<GenerateContentResponse> GenerateImageAsync(string prompt, ImageGenerationConfig config, bool includeText = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateImageAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }

            if (config == null)
            {
                Log.Error("Config is null in GenerateImageAsync.");
                throw new ArgumentNullException(nameof(config));
            }

            var request = new GenerateImageRequest
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
                generationConfig = new ImageGenerationConfig
                {
                    ResponseModalities = config.ResponseModalities != null && config.ResponseModalities.Any()
                        ? config.ResponseModalities
                        : includeText
                            ? new List<string> { "Text", "Image" }
                            : new List<string> { "Image" }
                }
            };

            try
            {
                Log.Information("Generating image with prompt: {Prompt}, includeText: {IncludeText}, modalities: {Modalities}",
                    prompt, includeText, string.Join(", ", request.generationConfig.ResponseModalities));
                var response = await _apiClient.GenerateContentAsync<GenerateImageRequest, GenerateContentResponse>(request, cancellationToken);
                Log.Information("Successfully generated image.");
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating image.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating image.");
                throw new Exception("An unexpected error occurred while generating image content.", ex);
            }
        }
    }
}
