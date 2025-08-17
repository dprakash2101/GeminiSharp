using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents the response from the Gemini API for content generation.
    /// </summary>
    public class GenerateContentResponse
    {
        /// <summary>
        /// Gets or sets the list of generated content candidates.
        /// </summary>
        [JsonPropertyName("candidates")]
        public List<Candidate>? Candidates { get; set; }

        /// <summary>
        /// Gets or sets metadata related to token usage.
        /// </summary>
        [JsonPropertyName("usageMetadata")]
        public UsageMetadata? UsageMetadata { get; set; }

        /// <summary>
        /// Gets or sets the version of the model used to generate the response.
        /// </summary>
        [JsonPropertyName("modelVersion")]
        public string? ModelVersion { get; set; }
    }
}