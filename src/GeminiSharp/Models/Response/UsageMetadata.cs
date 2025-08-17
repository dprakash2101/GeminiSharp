using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents metadata about token usage in a response.
    /// </summary>
    public class UsageMetadata
    {
        /// <summary>
        /// Gets or sets the number of tokens in the prompt.
        /// </summary>
        [JsonPropertyName("promptTokenCount")]
        public int PromptTokenCount { get; set; }

        /// <summary>
        /// Gets or sets the number of tokens in the generated candidates.
        /// </summary>
        [JsonPropertyName("candidatesTokenCount")]
        public int CandidatesTokenCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of tokens (prompt + candidates).
        /// </summary>
        [JsonPropertyName("totalTokenCount")]
        public int TotalTokenCount { get; set; }

        /// <summary>
        /// Gets or sets detailed information about prompt token usage.
        /// </summary>
        [JsonPropertyName("promptTokensDetails")]
        public List<TokenDetail>? PromptTokensDetails { get; set; }

        /// <summary>
        /// Gets or sets detailed information about candidate token usage.
        /// </summary>
        [JsonPropertyName("candidatesTokensDetails")]
        public List<TokenDetail>? CandidatesTokensDetails { get; set; }
    }
}