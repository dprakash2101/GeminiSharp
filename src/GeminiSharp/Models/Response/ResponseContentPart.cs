using GeminiSharp.Models.Request;
using Newtonsoft.Json;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents a part of the content block in the response.
    /// Can include text or inline image data (base64).
    /// </summary>
    public class ResponseContentPart
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

