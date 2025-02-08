using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a content block in the request.
    /// </summary>
    public class RequestContent
    {
        [JsonPropertyName("parts")]
        public List<RequestContentPart>? Parts { get; set; }
    }
}
