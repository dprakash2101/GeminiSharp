# 🛠️ Utility Client in GeminiSharp

The `UtilityClient` in GeminiSharp provides essential functionalities for interacting with the Gemini API beyond content generation, including embedding content, counting tokens, and retrieving model information. It is accessed via the main `GeminiClient` facade.

---

## 🔑 Key Functionalities

*   **`EmbedContentAsync`**: Generates numerical representations (embeddings) of text, useful for tasks like semantic search and text classification.
*   **`CountTokensAsync`**: Counts the number of tokens in a given text input, which is crucial for managing API costs and staying within model limits.
*   **`GetModelInfoAsync`**: Retrieves detailed information about a specific Gemini model, such as its capabilities and supported input/output types.

---

## 🧑‍💻 C# Example: Using Utility Client

This example demonstrates how to use the `UtilityClient` (accessed via `GeminiClient`) for its various functionalities.

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class UtilityClientExample
{
    public static async Task Run(string apiKey)
    {
        // Initialize GeminiClient (which provides access to UtilityClient)
        using var httpClient = new HttpClient();
        var geminiClient = new GeminiClient(apiKey, httpClient);

        Console.WriteLine("\n--- Utility Client Examples ---\n");

        // --- 1. Embed Content Example ---
        string embedText = "The quick brown fox jumps over the lazy dog.";
        string embedModel = "embedding-001"; // Specific model for embeddings
        Console.WriteLine($"Generating embeddings for text: \"{embedText}\" using model: {embedModel}");

        try
        {
            var embedResponse = await geminiClient.EmbedContentAsync(embedModel, embedText);
            if (embedResponse?.Embedding?.Values != null)
            {
                Console.WriteLine($"Successfully generated embedding with {embedResponse.Embedding.Values.Count} dimensions.");
                Console.WriteLine($"First 5 embedding values: {string.Join(", ", embedResponse.Embedding.Values.Take(5))}");
            }
            else
            {
                Console.WriteLine("Failed to generate embeddings.");
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error during embedding: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred during embedding: {ex.Message}");
        }

        Console.WriteLine("\n-----------------------------------\n");

        // --- 2. Count Tokens Example ---
        string countText = "This is a sample text to count tokens for.";
        string countModel = "gemini-2.5-flash"; // Model for token counting
        Console.WriteLine($"Counting tokens for text: \"{countText}\" using model: {countModel}");

        try
        {
            var countResponse = await geminiClient.CountTokensAsync(countModel, countText);
            if (countResponse?.TotalTokens != null)
            {
                Console.WriteLine($"Total tokens: {countResponse.TotalTokens}");
            }
            else
            {
                Console.WriteLine("Failed to count tokens.");
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error during token counting: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred during token counting: {ex.Message}");
        }

        Console.WriteLine("\n-----------------------------------\n");

        // --- 3. Get Model Info Example ---
        string modelInfoModel = "gemini-2.5-flash"; // Model to get info about
        Console.WriteLine($"Retrieving information for model: {modelInfoModel}");

        try
        {
            var modelInfo = await geminiClient.GetModelInfoAsync(modelInfoModel);
            if (modelInfo != null)
            {
                Console.WriteLine($"Model Name: {modelInfo.Name}");
                Console.WriteLine($"Display Name: {modelInfo.DisplayName}");
                Console.WriteLine($"Description: {modelInfo.Description}");
                Console.WriteLine($"Input Token Limit: {modelInfo.InputTokenLimit}");
                Console.WriteLine($"Output Token Limit: {modelInfo.OutputTokenLimit}");
                Console.WriteLine($"Supported Generation Methods: {string.Join(", ", modelInfo.SupportedGenerationMethods ?? new List<string>())}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve model information.");
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error retrieving model info: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred retrieving model info: {ex.Message}");
        }
    }
}

// To run this example:
// UtilityClientExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

```