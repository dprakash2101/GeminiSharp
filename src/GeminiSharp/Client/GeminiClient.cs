using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GeminiSharp.Models.Embeddings;
using GeminiSharp.Client.Clients;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides a high-level interface for the Gemini API, consolidating various functionalities.
    /// </summary>
    public class GeminiClient : IGeminiClient
    {
        private readonly GeminiApiClient _apiClient;

        

        private readonly TextClient _textClient;
        private readonly ImageClient _imageClient;
        private readonly StructuredContentClient _structuredContentClient;
        private readonly UtilityClient _utilityClient;
        private readonly DocumentClient _documentClient;
        private readonly AudioClient _audioClient;
        private readonly VideoClient _videoClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public GeminiClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _textClient = new TextClient(_apiClient);
            _imageClient = new ImageClient(_apiClient);
            _structuredContentClient = new StructuredContentClient(_apiClient);
            _utilityClient = new UtilityClient(_apiClient);
            _documentClient = new DocumentClient(_apiClient);
            _audioClient = new AudioClient(_apiClient);
            _videoClient = new VideoClient(_apiClient);
        }

        /// <summary>
        /// Generates text content based on a user prompt.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for content generation.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateTextAsync(string model, string prompt)
        {
            return await _textClient.GenerateTextAsync(model, prompt);
        }

        /// <summary>
        /// Generates an image from a text prompt using the Gemini image model.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for image generation.</param>
        /// <param name="generationConfig">Optional. Configuration for the generation process, including response modalities.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateImageAsync(string model, string prompt, GenerationConfig? generationConfig = null)
        {
            return await _imageClient.GenerateImageAsync(model, prompt, generationConfig);
        }

        /// <summary>
        /// Generates structured content based on a prompt and a JSON schema.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <param name="generationConfig">Optional. Configuration for the generation process, including response MIME type and schema.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the structured output.</returns>
        public async Task<GenerateContentResponse> GenerateStructuredContentAsync(string model, string prompt, object jsonSchema, GenerationConfig? generationConfig = null)
        {
            return await _structuredContentClient.GenerateStructuredContentAsync(model, prompt, jsonSchema, generationConfig);
        }

        /// <summary>
        /// Generates embeddings for a given text input.
        /// </summary>
        /// <param name="model">The Gemini model to use for embeddings (e.g., "embedding-001").</param>
        /// <param name="text">The text to generate embeddings for.</param>
        /// <returns>A <see cref="EmbeddingResponse"/> containing the generated embeddings.</returns>
        public async Task<EmbeddingResponse> EmbedContentAsync(string model, string text)
        {
            return await _utilityClient.EmbedContentAsync(model, text);
        }

        /// <summary>
        /// Counts the tokens in a given text input.
        /// </summary>
        /// <param name="model">The Gemini model to use for token counting.</param>
        /// <param name="text">The text to count tokens for.</param>
        /// <returns>A <see cref="CountTokensResponse"/> containing the token count.</returns>
        public async Task<CountTokensResponse> CountTokensAsync(string model, string text)
        {
            return await _utilityClient.CountTokensAsync(model, text);
        }

        /// <summary>
        /// Starts a new chat session.
        /// </summary>
        /// <param name="model">The model to use for the chat.</param>
        /// <returns>A new <see cref="ChatSession"/> instance.</returns>
        public ChatSession StartChat(string model)
        {
            return new ChatSession(model, _apiClient);
        }

        /// <summary>
        /// Retrieves information about a specific model.
        /// </summary>
        /// <param name="model">The model to get information about (e.g., "gemini-1.5-flash").</param>
        /// <returns>A <see cref="ModelInfo"/> object containing model details.</returns>
        public async Task<ModelInfo> GetModelInfoAsync(string model)
        {
            return await _utilityClient.GetModelInfoAsync(model);
        }

        /// <summary>
        /// Generates content from a text prompt and an image.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="imageData">The image data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the image (e.g., "image/jpeg").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromImageAsync(string model, string prompt, string imageData, string mimeType)
        {
            return await _imageClient.GenerateContentFromImageAsync(model, prompt, imageData, mimeType);
        }

        /// <summary>
        /// Generates content from a text prompt and a document.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="documentData">The document data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the document (e.g., "application/pdf").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromDocumentAsync(string model, string prompt, string documentData, string mimeType)
        {
            return await _documentClient.GenerateContentFromDocumentAsync(model, prompt, documentData, mimeType);
        }

        /// <summary>
        /// Generates content from a text prompt and a document URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="documentUri">The URI of the document.</param>
        /// <param name="mimeType">The MIME type of the document (e.g., "application/pdf").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromDocumentUriAsync(string model, string prompt, string documentUri, string mimeType)
        {
            return await _documentClient.GenerateContentFromDocumentUriAsync(model, prompt, documentUri, mimeType);
        }

        /// <summary>
        /// Generates content from a text prompt and a video.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="videoData">The video data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the video (e.g., "video/mp4").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromVideoAsync(string model, string prompt, string videoData, string mimeType)
        {
            return await _videoClient.GenerateContentFromVideoAsync(model, prompt, videoData, mimeType);
        }

        /// <summary>
        /// Generates content from a text prompt and a video URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="videoUri">The URI of the video.</param>
        /// <param name="mimeType">The MIME type of the video (e.g., "video/mp4").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromVideoUriAsync(string model, string prompt, string videoUri, string mimeType)
        {
            return await _videoClient.GenerateContentFromVideoUriAsync(model, prompt, videoUri, mimeType);
        }

        /// <summary>
        /// Generates content from a text prompt and an audio file.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="audioData">The audio data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the audio (e.g., "audio/mp3").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromAudioAsync(string model, string prompt, string audioData, string mimeType)
        {
            return await _audioClient.GenerateContentFromAudioAsync(model, prompt, audioData, mimeType);
        }

        /// <summary>
        /// Generates content from a text prompt and an audio URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="audioUri">The URI of the audio.</param>
        /// <param name="mimeType">The MIME type of the audio (e.g., "audio/mp3").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromAudioUriAsync(string model, string prompt, string audioUri, string mimeType)
        {
            return await _audioClient.GenerateContentFromAudioUriAsync(model, prompt, audioUri, mimeType);
        }

        /// <summary>
        /// Generates content with function calling.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="tools">A list of tools that the model can use.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentWithFunctionCallingAsync(string model, string prompt, List<Tool> tools)
        {
            return await _utilityClient.GenerateContentWithFunctionCallingAsync(model, prompt, tools);
        }

        /// <summary>
        /// Generates content from a URL.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="url">The URL to use as context.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        public async Task<GenerateContentResponse> GenerateContentFromUrlAsync(string model, string prompt, string url)
        {
            // This method is a special case as it uses GenerateTextAsync internally.
            // It will now delegate to the TextClient's GenerateTextAsync.
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromUrlAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                Log.Error("URL is empty or null in GenerateContentFromUrlAsync.");
                throw new ArgumentException("URL cannot be empty", nameof(url));
            }

            var fullPrompt = $"{prompt}\n\n{url}";

            return await _textClient.GenerateTextAsync(model, fullPrompt);
        }
    }
}