# 🎧 Audio Processing with GeminiSharp

GeminiSharp's `AudioClient` enables interaction with Gemini models for processing audio content. This includes generating content based on the auditory information within an audio file, either by providing the audio data directly (inline) or by referencing an audio file via a URI.

---

## 🔑 Key Functionalities

*   **`GenerateContentFromAudioAsync`**: Sends a text prompt along with the base64-encoded content of an audio file to the Gemini API. Useful for transcribing audio, summarizing spoken content, or extracting information from audio.
*   **`GenerateContentFromAudioUriAsync`**: Sends a text prompt along with a URI pointing to an audio file. This is ideal for large audio files or when the audio is already hosted and accessible by the Gemini API.

---

## 🧑‍💻 C# Example: Processing Audio

This example demonstrates how to use the `AudioClient` (accessed via `IGeminiClient` through DI) to generate content from both inline audio data and an audio URI.

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using GeminiSharp.Helpers;
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class AudioProcessingExample
{
    public static async Task Run(string apiKey)
    {
        // 1. Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(apiKey) // Replace with your actual API key
            .BuildServiceProvider();

        // 2. Resolve IGeminiClient from the container
        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        string model = "gemini-2.5-flash"; // Or a model suitable for audio understanding

        Console.WriteLine($"\n--- Audio Processing Examples (Model: {model}) ---\n");

        // --- 1. Generate Content from Inline Audio Data (Conceptual) ---
        Console.WriteLine("\n--- Example: Generating content from inline audio data (Conceptual) ---");
        string audioPath = "./sample_audio.mp3"; // Ensure this file exists for the example
        string audioMimeType = "audio/mp3";
        string audioPrompt = "Transcribe the spoken content in this audio.";

        // NOTE: Sending large audio files inline (Base64) is generally not recommended due to size limits.
        // This part is conceptual. For real-world scenarios, prefer using URIs for large files.
        if (!File.Exists(audioPath))
        {
            Console.WriteLine($"Creating a dummy audio file at {audioPath} for demonstration.");
            // In a real scenario, you would have a proper audio file.
            // For simplicity, we'll create a very basic text file and pretend it's an audio.
            // NOTE: Gemini API expects actual audio content, this is just for local file existence.
            File.WriteAllText(audioPath, "This is a sample audio content. It contains a short speech.");
        }

        try
        {
            // Convert audio to Base64 string (for small files only)
            string audioData = await FileConverter.ConvertFileToBase64Async(audioPath);

            Console.WriteLine($"Sending request for inline audio: {audioPath}");
            var response = await geminiClient.GenerateContentFromAudioAsync(
                model, audioPrompt, audioData, audioMimeType);

            Console.WriteLine("Response from inline audio:\n" + response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error processing inline audio: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred processing inline audio: {ex.Message}");
        }

        Console.WriteLine("\n-----------------------------------\n");

        // --- 2. Generate Content from Audio URI ---
        Console.WriteLine("\n--- Example: Generating content from audio URI ---");
        string audioUri = "gs://cloud-samples-data/speech/brooklyn_bridge.flac"; // Example public URI
        string uriMimeType = "audio/flac";
        string uriPrompt = "What is being discussed in this audio?";

        Console.WriteLine($"Sending request for audio URI: {audioUri}");
        try
        {
            var response = await geminiClient.GenerateContentFromAudioUriAsync(
                model, uriPrompt, audioUri, uriMimeType);

            Console.WriteLine("Response from audio URI:\n" + response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error processing audio URI: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred processing audio URI: {ex.Message}");
        }
    }
}

// To run this example:
// AudioProcessingExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## **🎯 Benefits of Audio Processing**

✅ **Transcription**: Convert spoken language into written text.
✅ **Audio Summarization**: Generate concise summaries of audio content.
✅ **Information Extraction**: Extract key information or topics from audio.
✅ **Flexible Input**: Support for both direct file upload (Base64 for small files) and cloud storage URIs.

---

## **📌 Important Considerations**

*   **File Size Limits**: Inline audio data (Base64) is subject to strict size limits. For larger audio files, always use the URI-based approach.
*   **URI Accessibility**: If using URIs, the Gemini API must have access to the specified URI. For Google Cloud Storage, ensure proper permissions.
*   **Processing Time**: Audio processing can be time-consuming, especially for longer audio files. Consider asynchronous processing and user feedback mechanisms.
*   **Privacy and Security**: When sending sensitive audio data, ensure you comply with data privacy regulations and Google's API usage policies.

---
