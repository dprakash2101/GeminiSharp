using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents detailed information about token usage for a specific modality.
    /// </summary>
    public class TokenDetail
    {
        /// <summary>
        /// Gets or sets the modality of the tokens (e.g., "TEXT", "IMAGE").
        /// </summary>
        [JsonPropertyName("modality")]
        public string? Modality { get; set; }

        /// <summary>
        /// Gets or sets the number of tokens for this modality.
        /// </summary>
        [JsonPropertyName("tokenCount")]
        public int TokenCount { get; set; }
    }
}