using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a part of a content block.
    /// </summary>
    public class RequestContentPart
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
