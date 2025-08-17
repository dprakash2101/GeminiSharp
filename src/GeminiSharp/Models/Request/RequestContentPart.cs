using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a part of a content block, which can be text or inline data.
    /// </summary>
    public class RequestContentPart
    {
        /// <summary>
        /// Gets or sets the text content of the part.
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the inline data content of the part, typically used for images.
        /// </summary>
        [JsonPropertyName("inlineData")]
        public InlineData? InlineData { get; set; }

        /// <summary>
        /// Gets or sets the file data content of the part, typically used for larger files.
        /// </summary>
        [JsonPropertyName("fileData")]
        public FileData? FileData { get; set; }
    }

    /// <summary>
    /// Represents inline image or binary data, typically base64 encoded.
    /// </summary>
    public class InlineData
    {
        /// <summary>
        /// Gets or sets the MIME type of the inline data (e.g., "image/png", "image/jpeg").
        /// </summary>
        [JsonPropertyName("mimeType")]
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the base64-encoded data.
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }

    /// <summary>
    /// Represents a file that has been uploaded to the API.
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// Gets or sets the MIME type of the file.
        /// </summary>
        [JsonPropertyName("mimeType")]
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the URI of the file.
        /// </summary>
        [JsonPropertyName("fileUri")]
        public string? FileUri { get; set; }
    }
}