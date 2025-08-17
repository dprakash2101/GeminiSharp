using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents metadata related to citations for generated content.
    /// </summary>
    public class CitationMetadata
    {
        /// <summary>
        /// Gets or sets the list of citation sources.
        /// </summary>
        [JsonPropertyName("citationSources")]
        public List<CitationSource>? CitationSources { get; set; }
    }
}