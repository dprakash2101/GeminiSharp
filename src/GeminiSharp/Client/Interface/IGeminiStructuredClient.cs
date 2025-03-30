using GeminiSharp.Models.Response;

namespace GeminiSharp.Client.Interface
{
    public interface IGeminiStructuredClient
    {
        /// <summary>
        /// Generates structured content asynchronously using the configured model and a JSON schema.
        /// </summary>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <returns>A task representing the structured content response.</returns>
        Task<GenerateContentResponse?> GenerateStructuredContentAsync(string prompt, object jsonSchema);
    }
}
