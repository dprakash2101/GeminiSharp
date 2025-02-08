using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents a part of the content block in the response.
    /// </summary>
    public class ResponseContentPart
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
