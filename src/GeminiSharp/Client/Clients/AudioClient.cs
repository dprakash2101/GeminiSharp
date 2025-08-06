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
    /// Provides functionality for generating content from audio using the Gemini API.
    /// </summary>
    public class AudioClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public AudioClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates content from a text prompt and an audio file.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="audioData">The audio data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the audio (e.g., "audio/mp3").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, audioData, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromAudioAsync(string? model, string prompt, string audioData, string mimeType)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromAudioAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(audioData))
            {
                Log.Error("Audio data is empty or null in GenerateContentFromAudioAsync.");
                throw new ArgumentException("Audio data cannot be empty", nameof(audioData));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromAudioAsync.");
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
                                    Data = audioData
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from audio for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from audio for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from audio for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from audio for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from audio.", ex);
            }
        }

        /// <summary>
        /// Generates content from a text prompt and an audio URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="audioUri">The URI of the audio.</param>
        /// <param name="mimeType">The MIME type of the audio (e.g., "audio/mp3").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, audioUri, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromAudioUriAsync(string? model, string prompt, string audioUri, string mimeType)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromAudioUriAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(audioUri))
            {
                Log.Error("Audio URI is empty or null in GenerateContentFromAudioUriAsync.");
                throw new ArgumentException("Audio URI cannot be empty", nameof(audioUri));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromAudioUriAsync.");
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
                                    FileUri = audioUri
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from audio URI for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from audio URI for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from audio URI for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from audio URI for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from audio URI.", ex);
            }
        }
    }
}