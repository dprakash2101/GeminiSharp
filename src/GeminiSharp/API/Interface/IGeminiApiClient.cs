using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

namespace GeminiSharp.API.Interface
{
    public interface IGeminiApiClient
    {
        Task<GenerateContentResponse> GenerateContentAsync(string model, GenerateContentRequest request);
        Task<GenerateContentResponse> GenerateStructuredContentAsync(string model, GeminiStructuredRequest request);
    }
}
