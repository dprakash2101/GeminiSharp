using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents the request payload for generating content, images, or structured output.
    /// </summary>
    public class GenerateContentRequest
    {
        /// <summary>
        /// Gets or sets the list of content blocks for the request.
        /// </summary>
        [JsonPropertyName("contents")]
        public List<RequestContent>? Contents { get; set; }

        /// <summary>
        /// Gets or sets the generation configuration for the request.
        /// </summary>
        [JsonPropertyName("generationConfig")]
        public GenerationConfig? GenerationConfig { get; set; }

        /// <summary>
        /// Gets or sets the list of tools that the model can use.
        /// </summary>
        [JsonPropertyName("tools")]
        public List<Tool>? Tools { get; set; }
    }
}
