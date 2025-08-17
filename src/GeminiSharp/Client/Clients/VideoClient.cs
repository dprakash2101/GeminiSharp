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
    /// Provides functionality for generating content from video using the Gemini API.
    /// </summary>
    public class VideoClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public VideoClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates content from a text prompt and a video.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="videoData">The video data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the video (e.g., "video/mp4").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, videoData, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromVideoAsync(string? model, string prompt, string videoData, string mimeType)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
                if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromVideoAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(videoData))
            {
                Log.Error("Video data is empty or null in GenerateContentFromVideoAsync.");
                throw new ArgumentException("Video data cannot be empty", nameof(videoData));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromVideoAsync.");
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
                                    Data = videoData
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from video for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from video for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from video for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from video for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from video.", ex);
            }
        }

        /// <summary>
        /// Generates content from a text prompt and a video URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="videoUri">The URI of the video.</param>
        /// <param name="mimeType">The MIME type of the video (e.g., "video/mp4").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, videoUri, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromVideoUriAsync(string? model, string prompt, string videoUri, string mimeType)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
                if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromVideoUriAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(videoUri))
            {
                Log.Error("Video URI is empty or null in GenerateContentFromVideoUriAsync.");
                throw new ArgumentException("Video URI cannot be empty", nameof(videoUri));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromVideoUriAsync.");
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
                                FileData = new FileData
                                {
                                    MimeType = mimeType,
                                    FileUri = videoUri
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from video URI for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from video URI for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from video URI for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from video URI for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from video URI.", ex);
            }
        }
    }
}