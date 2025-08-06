using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a content block in the request, typically containing parts of a message.
    /// </summary>
    public class RequestContent
    {
        /// <summary>
        /// Gets or sets the list of parts that make up the content block.
        /// </summary>
        [JsonPropertyName("parts")]
        public List<RequestContentPart>? Parts { get; set; }
    }
}