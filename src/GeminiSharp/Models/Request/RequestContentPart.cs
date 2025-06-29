using GeminiSharp.Models.Response;
using Newtonsoft.Json;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a part of a content block.
    /// </summary>
    public class RequestContentPart
    {
        /// <summary>
        /// The text content of the part.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        /// The inline data of the part.
        /// </summary>
        [JsonProperty("inlineData", NullValueHandling = NullValueHandling.Ignore)]
        public InlineData InlineData { get; set; }

        /// <summary>
        /// The file data of the part.
        /// </summary>
        [JsonProperty("fileData", NullValueHandling = NullValueHandling.Ignore)]
        public FileData FileData { get; set; }
    }
}
