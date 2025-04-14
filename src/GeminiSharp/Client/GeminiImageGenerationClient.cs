using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides an interface for generating images using the Gemini API.
    /// </summary>
    public class GeminiImageGenerationClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiImageGenerationClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        public GeminiImageGenerationClient(string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(apiKey, baseUrl);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiImageGenerationClient"/> class with a custom HttpClient.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests.</param>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional).</param>
        public GeminiImageGenerationClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(httpClient, apiKey, baseUrl);
        }

        /// <summary>
        /// Generates an image from a text prompt using the Gemini image model.
        /// </summary>
        /// <param name="prompt">The image generation prompt.</param>
        /// <param name="config">The configuration for image generation.</param>
        /// <param name="model">The Gemini model to use (default is "gemini-2.0-flash-exp-image-generation").</param>
        /// <param name="includeText">Whether to include a text response along with the image (default is false).</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the image (and optional text).</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the config is null.</exception>
        public async Task<GenerateContentResponse> GenerateImageAsync(string prompt, ImageGenerationConfig config, string model = "gemini-2.0-flash-exp-image-generation", bool includeText = false)
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
                Log.Information("Generating image for model {Model} with prompt: {Prompt}, includeText: {IncludeText}, modalities: {Modalities}",
                    model, prompt, includeText, string.Join(", ", request.generationConfig.ResponseModalities));
                var response = await _apiClient.SendRequestAsync(model, request, "generateContent");
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
}