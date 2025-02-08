using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents the request payload for generating content.
    /// </summary>
    public class GenerateContentRequest
    {
        [JsonPropertyName("contents")]
        public List<RequestContent>? Contents { get; set; }
    }
}
