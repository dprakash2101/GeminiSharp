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
    /// Provides functionality for generating image content using the Gemini API.
    /// </summary>
    public class ImageClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public ImageClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates an image from a text prompt using the Gemini image model.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for image generation.</param>
        /// <param name="generationConfig">Optional. Configuration for the generation process, including response modalities.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateImageAsync(string? model, string prompt, GenerationConfig? generationConfig = null)
        {
            model = model ?? "gemini-2.0-flash-preview-image-generation"; // Set default model
                if (string.IsNullOrWhiteSpace(prompt))
                {
                    Log.Error("Prompt is empty or null in GenerateImageAsync.");
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
                },
                    GenerationConfig = generationConfig ?? new GenerationConfig { ResponseModalities = new List<string> { "IMAGE" } }
                };

                try
                {
                    Log.Information("Generating image for model {Model} with prompt: {Prompt}, modalities: {Modalities}",
                        model, prompt, string.Join(", ", request.GenerationConfig?.ResponseModalities ?? new List<string>()));
                    var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                    Log.Information("Successfully generated image for model {Model}.", model);
                    return response;
                }
                catch (GeminiApiException ex)
                {
                    Log.Error(ex, "API error while generating image for model {Model}.", model);
                    throw;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Unexpected error while generating image for model {Model}.", model);
                    throw new Exception("An unexpected error occurred while generating image content.", ex);
                }
            }
        }

        /// <summary>
        /// Generates content from a text prompt and an image.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="imageData">The image data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the image (e.g., "image/jpeg").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, imageData, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromImageAsync(string model, string prompt, string imageData, string mimeType)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromImageAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(imageData))
            {
                Log.Error("Image data is empty or null in GenerateContentFromImageAsync.");
                throw new ArgumentException("Image data cannot be empty", nameof(imageData));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromImageAsync.");
                throw new ArgumentException("MIME type cannot be empty", nameof(mimeType));
            }

            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart>
                        {
                            new RequestContentPart { Text = prompt },
                            new RequestContentPart
                            {
                                InlineData = new InlineData
                                {
                                    MimeType = mimeType,
                                    Data = imageData
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from image for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from image for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from image for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from image for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from image.", ex);
            }
        }
    }