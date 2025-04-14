namespace GeminiSharp.Models.Request
{
    public class GenerateImageRequest
    {
        public List<RequestContent> Contents { get; set; } = new();
        public ImageGenerationConfig? generationConfig { get; set; } 

    }
    public class ImageGenerationConfig
    {
        public List<string>? ResponseModalities { get; set; }
    }
}
