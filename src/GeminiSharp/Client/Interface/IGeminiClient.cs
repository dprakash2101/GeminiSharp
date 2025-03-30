using System.Threading.Tasks;
using GeminiSharp.Models.Response;

namespace GeminiSharp.Client.Interface
{
    /// <summary>
    /// Defines an interface for interacting with the Gemini API to generate both text and structured content.
    /// </summary>
    public interface IGeminiClient
    {
        /// <summary>
        /// Generates text-based content asynchronously using the configured model.
        /// </summary>
        /// <param name="prompt">The user prompt to generate content from.</param>
        /// <returns>A task representing the generated content response.</returns>
        Task<GenerateContentResponse?> GenerateContentAsync(string prompt);

        
    }
}
