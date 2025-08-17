# 🎥 Video Processing with GeminiSharp

GeminiSharp's `VideoClient` enables interaction with Gemini models for processing video content. This includes generating content based on the visual and auditory information within a video, either by providing the video data directly (inline) or by referencing a video via a URI.

---

## 🔑 Key Functionalities

*   **`GenerateContentFromVideoAsync`**: Sends a text prompt along with the base64-encoded content of a video to the Gemini API. Useful for analyzing scenes, identifying objects, or summarizing events within videos.
*   **`GenerateContentFromVideoUriAsync`**: Sends a text prompt along with a URI pointing to a video. This is ideal for large video files or when the video is already hosted and accessible by the Gemini API.

---

## 🧑‍💻 C# Example: Processing Videos

This example demonstrates how to use the `VideoClient` (accessed via `IGeminiClient` through DI) to generate content from both inline video data and a video URI.

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

public class VideoProcessingExample
{
    public static async Task Run(string apiKey)
    {
        // 1. Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(apiKey) // Replace with your actual API key
            .BuildServiceProvider();

        // 2. Resolve IGeminiClient from the container
        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        string model = "gemini-2.5-flash"; // Or a model suitable for video understanding

        Console.WriteLine($"\n--- Video Processing Examples (Model: {model}) ---\n");

        // --- 1. Generate Content from Inline Video Data (Conceptual) ---
        Console.WriteLine("\n--- Example: Generating content from inline video data (Conceptual) ---");
        string videoPath = "./sample_video.mp4"; // Ensure this file exists for the example
        string videoMimeType = "video/mp4";
        string videoPrompt = "Describe the main actions happening in this video.";

        // NOTE: Sending large video files inline (Base64) is generally not recommended due to size limits.
        // This part is conceptual. For real-world scenarios, prefer using URIs for large files.
        if (!File.Exists(videoPath))
        {
            Console.WriteLine($"Creating a dummy video file at {videoPath} for demonstration.");
            // In a real scenario, you would have a proper video file.
            // For simplicity, we'll create a very basic text file and pretend it's a video.
            // NOTE: Gemini API expects actual video content, this is just for local file existence.
            File.WriteAllText(videoPath, "This is a sample video content. It shows a cat playing with a ball.");
        }

        try
        {
            // Convert video to Base64 string (for small files only)
            string videoData = await FileConverter.ConvertFileToBase64Async(videoPath);

            Console.WriteLine($"Sending request for inline video: {videoPath}");
            var response = await geminiClient.GenerateContentFromVideoAsync(
                model, videoPrompt, videoData, videoMimeType);

            Console.WriteLine("Response from inline video:\n" + response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error processing inline video: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred processing inline video: {ex.Message}");
        }

        Console.WriteLine("\n-----------------------------------\n");

        // --- 2. Generate Content from Video URI ---
        Console.WriteLine("\n--- Example: Generating content from video URI ---");
        string videoUri = "gs://cloud-samples-data/video/animals.mp4"; // Example public URI
        string uriMimeType = "video/mp4";
        string uriPrompt = "What animals are visible in this video?";

        Console.WriteLine($"Sending request for video URI: {videoUri}");
        try
        {
            var response = await geminiClient.GenerateContentFromVideoUriAsync(
                model, uriPrompt, videoUri, uriMimeType);

            Console.WriteLine("Response from video URI:\n" + response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error processing video URI: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred processing video URI: {ex.Message}");
        }
    }
}

// To run this example:
// VideoProcessingExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## **🎯 Benefits of Video Processing**

✅ **Video Summarization**: Generate concise summaries of video content.
✅ **Event Detection**: Identify and describe key events or actions within videos.
✅ **Object Recognition**: Pinpoint and describe objects or entities appearing in video frames.
✅ **Flexible Input**: Support for both direct file upload (Base64 for small files) and cloud storage URIs.

---

## **📌 Important Considerations**

*   **File Size Limits**: Inline video data (Base64) is subject to strict size limits. For larger videos, always use the URI-based approach.
*   **URI Accessibility**: If using URIs, the Gemini API must have access to the specified URI. For Google Cloud Storage, ensure proper permissions.
*   **Processing Time**: Video processing can be time-consuming, especially for longer videos. Consider asynchronous processing and user feedback mechanisms.
*   **Privacy and Security**: When sending sensitive video data, ensure you comply with data privacy regulations and Google's API usage policies.

---