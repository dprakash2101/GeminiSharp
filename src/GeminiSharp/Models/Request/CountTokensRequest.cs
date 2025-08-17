using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents the request payload for counting tokens.
    /// </summary>
    public class CountTokensRequest
    {
        /// <summary>
        /// Gets or sets the list of content blocks for which to count tokens.
        /// </summary>
        [JsonPropertyName("contents")]
        public List<RequestContent>? Contents { get; set; }
    }
}