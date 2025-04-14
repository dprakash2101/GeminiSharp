using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    public class GenerateImageRequest
    {
        [JsonPropertyName("contents")]
        public List<RequestContent> Contents { get; set; } = new();
        [JsonPropertyName("generationConfig")]
        public ImageGenerationConfig? generationConfig { get; set; } 

    }
    public class ImageGenerationConfig
    {
        [JsonPropertyName("responseModalities")]
        public List<string>? ResponseModalities { get; set; }
    }
}
