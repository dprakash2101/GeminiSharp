using System.Text.Json.Serialization;
using GeminiSharp.Models.Request;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents a part of the content block in the response.
    /// Can include text or inline image data (base64).
    /// </summary>
    public class ResponseContentPart
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
        /// Gets or sets the function call that the model wants to make.
        /// </summary>
        [JsonPropertyName("functionCall")]
        public FunctionCall? FunctionCall { get; set; }
    }
}