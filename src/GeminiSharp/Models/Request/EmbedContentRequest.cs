using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents the request payload for embedding content.
    /// </summary>
    public class EmbedContentRequest
    {
        /// <summary>
        /// Gets or sets the content to be embedded.
        /// </summary>
        [JsonPropertyName("content")]
        public RequestContent? Content { get; set; }
    }
}