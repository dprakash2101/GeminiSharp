using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Defines the configuration for content generation, including response format and modalities.
    /// </summary>
    public class GenerationConfig
    {
        /// <summary>
        /// Gets or sets the desired MIME type for the response (e.g., "application/json").
        /// </summary>
        [JsonPropertyName("response_mime_type")]
        public string? response_mime_type { get; set; }

        /// <summary>
        /// Gets or sets the JSON schema that defines the structured output format.
        /// </summary>
        [JsonPropertyName("response_schema")]
        public object? response_schema { get; set; }

        /// <summary>
        /// Gets or sets the desired response modalities (e.g., "TEXT", "IMAGE").
        /// </summary>
        [JsonPropertyName("responseModalities")]
        public List<string>? ResponseModalities { get; set; }
    }
}
