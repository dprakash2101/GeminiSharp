using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents a part of the content block in the response.
    /// Can include text or inline image data (base64).
    /// </summary>
    public class ResponseContentPart
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("inlineData")]
        public InlineData? InlineData { get; set; }
    }

    
}
