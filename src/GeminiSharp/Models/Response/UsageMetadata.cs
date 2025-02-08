using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents metadata about token usage.
    /// </summary>
    public class UsageMetadata
    {
        [JsonPropertyName("promptTokenCount")]
        public int PromptTokenCount { get; set; }

        [JsonPropertyName("candidatesTokenCount")]
        public int CandidatesTokenCount { get; set; }

        [JsonPropertyName("totalTokenCount")]
        public int TotalTokenCount { get; set; }

        [JsonPropertyName("promptTokensDetails")]
        public List<TokenDetail>? PromptTokensDetails { get; set; }

        [JsonPropertyName("candidatesTokensDetails")]
        public List<TokenDetail>? CandidatesTokensDetails { get; set; }
    }
}
