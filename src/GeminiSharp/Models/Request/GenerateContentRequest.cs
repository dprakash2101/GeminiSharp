using Newtonsoft.Json;
using System.Collections.Generic;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents the request payload for generating content.
    /// </summary>
    public class GenerateContentRequest
    {
        /// <summary>
        /// The contents of the request.
        /// </summary>
        [JsonProperty("contents")]
        public List<RequestContent> Contents { get; set; }

        /// <summary>
        /// The generation configuration for the request.
        /// </summary>
        [JsonProperty("generationConfig", NullValueHandling = NullValueHandling.Ignore)]
        public GenerationConfig GenerationConfig { get; set; }
    }
}
