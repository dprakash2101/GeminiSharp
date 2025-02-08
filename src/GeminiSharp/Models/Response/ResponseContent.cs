using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents the actual content of the response.
    /// </summary>
    public class ResponseContent
    {
        [JsonPropertyName("parts")]
        public List<ResponseContentPart>? Parts { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}
