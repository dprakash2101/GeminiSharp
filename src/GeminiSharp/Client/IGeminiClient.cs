using GeminiSharp.Models.Response;
using System.Threading.Tasks;
using System.Collections.Generic;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Embeddings;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Represents the contract for a unified Gemini API client.
    /// </summary>
    public interface IGeminiClient
    {
        /// <summary>
        /// Generates text content based on a user prompt.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for content generation.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateTextAsync(string? model, string prompt);

        /// <summary>
        /// Generates an image from a text prompt using the Gemini image model.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The input prompt for image generation.</param>
        /// <param name="generationConfig">Optional. Configuration for the generation process, including response modalities.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateImageAsync(string? model, string prompt, GenerationConfig? generationConfig = null);

        /// <summary>
        /// Generates structured content based on a prompt and a JSON schema.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <param name="generationConfig">Optional. Configuration for the generation process, including response MIME type and schema.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the structured output.</returns>
        Task<GenerateContentResponse> GenerateStructuredContentAsync(string? model, string prompt, object jsonSchema, GenerationConfig? generationConfig = null);

        /// <summary>
        /// Generates embeddings for a given text input.
        /// </summary>
        /// <param name="model">The Gemini model to use for embeddings (e.g., "embedding-001").</param>
        /// <param name="text">The text to generate embeddings for.</param>
        /// <returns>A <see cref="EmbeddingResponse"/> containing the generated embeddings.</returns>
        Task<EmbeddingResponse> EmbedContentAsync(string? model, string text);

        /// <summary>
        /// Counts the tokens in a given text input.
        /// </summary>
        /// <param name="model">The Gemini model to use for token counting.</param>
        /// <param name="text">The text to count tokens for.</param>
        /// <returns>A <see cref="CountTokensResponse"/> containing the token count.</returns>
        Task<CountTokensResponse> CountTokensAsync(string? model, string text);

        /// <summary>
        /// Starts a new chat session.
        /// </summary>
        /// <param name="model">The model to use for the chat.</param>
        /// <returns>A new <see cref="ChatSession"/> instance.</returns>
        ChatSession StartChat(string model);

        /// <summary>
        /// Retrieves information about a specific model.
        /// </summary>
        /// <param name="model">The model to get information about (e.g., "gemini-1.5-flash").</param>
        /// <returns>A <see cref="ModelInfo"/> object containing model details.</returns>
        Task<ModelInfo> GetModelInfoAsync(string model);

        /// <summary>
        /// Generates content from a text prompt and an image.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="imageData">The image data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the image (e.g., "image/jpeg").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromImageAsync(string? model, string prompt, string imageData, string mimeType);

        /// <summary>
        /// Generates content from a text prompt and a document.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="documentData">The document data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the document (e.g., "application/pdf").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromDocumentAsync(string? model, string prompt, string documentData, string mimeType);

        /// <summary>
        /// Generates content from a text prompt and a document URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="documentUri">The URI of the document.</param>
        /// <param name="mimeType">The MIME type of the document (e.g., "application/pdf").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromDocumentUriAsync(string? model, string prompt, string documentUri, string mimeType);

        /// <summary>
        /// Generates content from a text prompt and a video.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="videoData">The video data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the video (e.g., "video/mp4").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromVideoAsync(string? model, string prompt, string videoData, string mimeType);

        /// <summary>
        /// Generates content from a text prompt and a video URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="videoUri">The URI of the video.</param>
        /// <param name="mimeType">The MIME type of the video (e.g., "video/mp4").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromVideoUriAsync(string? model, string prompt, string videoUri, string mimeType);

        Task<GenerateContentResponse> GenerateContentFromAudioAsync(string? model, string prompt, string audioData, string mimeType);

        /// <summary>
        /// Generates content from a text prompt and an audio URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="audioUri">The URI of the audio.</param>
        /// <param name="mimeType">The MIME type of the audio (e.g., "audio/mp3").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromAudioUriAsync(string? model, string prompt, string audioUri, string mimeType);

        

        /// <summary>
        /// Generates content from a text prompt and an audio URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="audioUri">The URI of the audio.</param>
        /// <param name="mimeType">The MIME type of the audio (e.g., "audio/mp3").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromAudioUriAsync(string model, string prompt, string audioUri, string mimeType);

        /// <summary>
        /// Generates content with function calling.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="tools">A list of tools that the model can use.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentWithFunctionCallingAsync(string? model, string prompt, List<Tool> tools);

        /// <summary>
        /// Generates content from a URL.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="url">The URL to use as context.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        Task<GenerateContentResponse> GenerateContentFromUrlAsync(string model, string prompt, string url);
    }
}