# 📄 Document Processing with GeminiSharp

GeminiSharp's `DocumentClient` facilitates interaction with Gemini models for processing documents. This includes generating content based on the content of a document, either by providing the document data directly (inline) or by referencing a document via a URI.

---

## 🔑 Key Functionalities

*   **`GenerateContentFromDocumentAsync`**: Sends a text prompt along with the base64-encoded content of a document to the Gemini API. Useful for analyzing, summarizing, or extracting information from documents directly.
*   **`GenerateContentFromDocumentUriAsync`**: Sends a text prompt along with a URI pointing to a document. This is ideal for large documents or when the document is already hosted and accessible by the Gemini API.

---

## 🧑‍💻 C# Example: Processing Documents

This example demonstrates how to use the `DocumentClient` (accessed via `GeminiClient`) to generate content from both inline document data and a document URI.

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using GeminiSharp.Helpers;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class DocumentProcessingExample
{
    public static async Task Run(string apiKey)
    {
        // Initialize GeminiClient (which provides access to DocumentClient)
        using var httpClient = new HttpClient();
        var geminiClient = new GeminiClient(apiKey, httpClient);

        string model = "gemini-2.5-flash"; // Or a model suitable for document understanding

        Console.WriteLine($"\n--- Document Processing Examples (Model: {model}) ---\n");

        // --- 1. Generate Content from Inline Document Data ---
        Console.WriteLine("\n--- Example: Generating content from inline document data ---");
        string documentPath = "./sample_document.pdf"; // Ensure this file exists for the example
        string documentMimeType = "application/pdf";
        string documentPrompt = "Summarize the key points of this document.";

        // Create a dummy PDF file for demonstration if it doesn't exist
        if (!File.Exists(documentPath))
        {
            Console.WriteLine($"Creating a dummy PDF file at {documentPath} for demonstration.");
            // In a real scenario, you would have a proper PDF file.
            // For simplicity, we'll create a very basic text file and pretend it's a PDF.
            // NOTE: Gemini API expects actual PDF content, this is just for local file existence.
            File.WriteAllText(documentPath, "This is a sample document content. It discusses various aspects of AI and machine learning.");
        }

        try
        {
            // Convert document to Base64 string
            string documentData = await FileConverter.ConvertFileToBase64Async(documentPath);

            Console.WriteLine($"Sending request for inline document: {documentPath}");
            var response = await geminiClient.GenerateContentFromDocumentAsync(
                model, documentPrompt, documentData, documentMimeType);

            Console.WriteLine("Response from inline document:\n" + response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error processing inline document: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred processing inline document: {ex.Message}");
        }

        Console.WriteLine("\n-----------------------------------\n");

        // --- 2. Generate Content from Document URI ---
        Console.WriteLine("\n--- Example: Generating content from document URI ---");
        string documentUri = "gs://cloud-samples-data/generative-ai/pdf/2403.05530.pdf"; // Example public URI
        string uriMimeType = "application/pdf";
        string uriPrompt = "What is the main topic of the document at this URI?";

        Console.WriteLine($"Sending request for document URI: {documentUri}");
        try
        {
            var response = await geminiClient.GenerateContentFromDocumentUriAsync(
                model, uriPrompt, documentUri, uriMimeType);

            Console.WriteLine("Response from document URI:\n" + response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error processing document URI: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred processing document URI: {ex.Message}");
        }
    }
}

// To run this example:
// DocumentProcessingExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## **🎯 Benefits of Document Processing**

✅ **Content Summarization**: Quickly get summaries of long documents without manual reading.
✅ **Information Extraction**: Extract specific data points, entities, or facts from documents.
✅ **Question Answering**: Ask questions about document content and receive relevant answers.
✅ **Flexible Input**: Support for both direct file upload (Base64) and cloud storage URIs.

---

## **📌 Important Considerations**

*   **File Formats**: Ensure the MIME type provided matches the actual document type. Gemini API supports various document types (e.g., PDF, DOCX, TXT).
*   **URI Accessibility**: If using URIs, the Gemini API must have access to the specified URI. For Google Cloud Storage, ensure proper permissions.
*   **Token Limits**: Large documents can consume a significant number of tokens. Be mindful of the model's input token limits.
*   **Privacy and Security**: When sending sensitive document data, ensure you comply with data privacy regulations and Google's API usage policies.

---
