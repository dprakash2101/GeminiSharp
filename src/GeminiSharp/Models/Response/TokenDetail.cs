using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{

    /// <summary>
    /// Represents detailed information about token usage.
    /// </summary>
    public class TokenDetail
    {
        [JsonPropertyName("modality")]
        public string? Modality { get; set; }

        [JsonPropertyName("tokenCount")]
        public int TokenCount { get; set; }
    }
}
