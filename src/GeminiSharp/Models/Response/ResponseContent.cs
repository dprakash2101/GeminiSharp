using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents the actual content of the response, composed of multiple parts.
    /// </summary>
    public class ResponseContent
    {
        /// <summary>
        /// Gets or sets the list of parts that make up the content block.
        /// </summary>
        [JsonPropertyName("parts")]
        public List<ResponseContentPart>? Parts { get; set; }

        /// <summary>
        /// Gets or sets the role of the author of this content (e.g., "user", "model").
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}