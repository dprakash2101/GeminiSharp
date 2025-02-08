using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents metadata related to citations.
    /// </summary>
    public class CitationMetadata
    {
        [JsonPropertyName("citationSources")]
        public List<CitationSource>? CitationSources { get; set; }
    }
}
