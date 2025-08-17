using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents a single citation source for generated content.
    /// </summary>
    public class CitationSource
    {
        /// <summary>
        /// Gets or sets the starting index of the cited text in the generated content.
        /// </summary>
        [JsonPropertyName("startIndex")]
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the ending index of the cited text in the generated content.
        /// </summary>
        [JsonPropertyName("endIndex")]
        public int EndIndex { get; set; }

        /// <summary>
        /// Gets or sets the URI of the citation source.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }
    }
}