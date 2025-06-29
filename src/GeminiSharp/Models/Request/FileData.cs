using Newtonsoft.Json;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents file data for a content part.
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// The MIME type of the file.
        /// </summary>
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// The URI of the file.
        /// </summary>
        [JsonProperty("fileUri")]
        public string FileUri { get; set; }
    }
}
