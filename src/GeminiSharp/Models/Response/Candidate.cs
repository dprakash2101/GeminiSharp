using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents an individual candidate response.
    /// </summary>
    public class Candidate
    {
        [JsonPropertyName("content")]
        public ResponseContent? Content { get; set; }

        [JsonPropertyName("finishReason")]
        public string? FinishReason { get; set; }

        [JsonPropertyName("citationMetadata")]
        public CitationMetadata? CitationMetadata { get; set; }

        [JsonPropertyName("avgLogprobs")]
        public double? AvgLogprobs { get; set; }
    }
}
