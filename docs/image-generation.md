# 🖼️ Image Generation with GeminiSharp

This guide explains how to use the **GeminiSharp SDK** to generate images using Google's Gemini API.

## ✨ Core Concept

Image generation involves sending a text prompt (a description of the desired image) to a compatible Gemini model via the SDK. The API processes the prompt and returns image data, typically encoded in Base64 format. The SDK's `ImageClient` simplifies this process, handling the API communication and data parsing.

---

## 🔑 Key Components

*   **`ImageClient`**: The specialized client class for interacting with Gemini image generation endpoints. It is instantiated via the main `GeminiClient`.
*   **`GenerationConfig`**: A class to specify configuration options for the generation request, such as `ResponseModalities` to ensure image output.
*   **`FileConverter.ConvertBase64ToImageStream`**: A utility function to convert the Base64 image data received from the API into a usable `MemoryStream`.
*   **Model**: You can specify a model for image generation. If no model is provided, the `ImageClient` will default to `gemini-2.5-flash`.

---

## 🧑‍💻 C# Example: Generating and Saving an Image

This example demonstrates how to use the `ImageClient` (accessed via `GeminiClient`) to generate an image from a text prompt and save it to a file.

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Models.Request;
using System;
using System.IO;
using System.Threading.Tasks;

public class ImageGenerationExample
{
    public static async Task Run(string apiKey)
    {
        // Initialize GeminiClient (which provides access to ImageClient)
        using var httpClient = new HttpClient();
        var geminiClient = new GeminiClient(apiKey, httpClient);

        string prompt = "A futuristic city at sunset, with flying cars and neon lights.";
        string outputPath = "generated_image.png";

        Console.WriteLine($"Generating image for prompt: \"{prompt}\"");

        try
        {
            // Generate the image using the ImageClient (accessed via geminiClient)
            // You can pass null for the model to use the default (gemini-2.5-flash)
            var response = await geminiClient.GenerateImageAsync(
                null, // Use default model (gemini-2.5-flash)
                prompt,
                new GenerationConfig { ResponseModalities = new List<string> { "IMAGE" } } // Ensure image output
            );

            // Extract Base64 image data from the response
            var imagePart = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault(p => p.InlineData != null);
            var base64Image = imagePart?.InlineData?.Data;
            var mimeType = imagePart?.InlineData?.MimeType ?? "image/png";

            if (string.IsNullOrWhiteSpace(base64Image))
            {
                Console.WriteLine("Error: No image data received from the API.");
                return;
            }

            // Convert Base64 string to MemoryStream and save to file
            using (var imageStream = FileConverter.ConvertBase64ToImageStream(base64Image, mimeType))
            {
                if (imageStream != null)
                {
                    using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        await imageStream.CopyToAsync(fileStream);
                    }
                    Console.WriteLine($"Image successfully saved to {outputPath}");
                }
                else
                {
                    Console.WriteLine("Error: Failed to convert image data.");
                }
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}

// To run this example:
// ImageGenerationExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## 🌐 .NET Web API Integration Example

This example demonstrates how to integrate the `ImageClient` into an ASP.NET Core controller endpoint to generate and return an image. The API key is passed via a header for simplicity (in production, use secure configuration methods).

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiApiExamples.Controllers // Adjust namespace as needed
{
    public class GenerateImageRequest
    {
        public string Prompt { get; set; } = string.Empty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpPost("generate")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateImage(
            [FromBody] GenerateImageRequest request,
            [FromHeader(Name = "X-Gemini-ApiKey")] string apiKey)
        {
            if (string.IsNullOrWhiteSpace(request?.Prompt))
            {
                _logger.LogWarning("Image generation request received with empty prompt.");
                return BadRequest("Prompt cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogWarning("Image generation request received without API key.");
                return Problem("X-Gemini-ApiKey header is required.", statusCode: StatusCodes.Status400BadRequest);
            }

            try
            {
                _logger.LogInformation("Generating image for prompt: {Prompt}", request.Prompt);

                // Initialize GeminiClient
                using var httpClient = new HttpClient();
                var geminiClient = new GeminiClient(apiKey, httpClient);

                // Generate the image using the ImageClient
                var response = await geminiClient.GenerateImageAsync(
                    null, // Use default model (gemini-2.5-flash)
                    request.Prompt,
                    new GenerationConfig { ResponseModalities = new List<string> { "IMAGE" } }
                );

                var imagePart = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault(p => p.InlineData != null);
                var base64Image = imagePart?.InlineData?.Data;
                var mimeType = imagePart?.InlineData?.MimeType ?? "image/png";

                if (string.IsNullOrWhiteSpace(base64Image))
                {
                    _logger.LogError("No image data found in API response for prompt: {Prompt}. Response: {@Response}", request.Prompt, response);
                    return Problem("Failed to generate image: No image data received from API.", statusCode: StatusCodes.Status502BadGateway);
                }

                using (var imageStream = FileConverter.ConvertBase64ToImageStream(base64Image, mimeType))
                {
                    if (imageStream == null)
                    {
                        _logger.LogError("Failed to convert Base64 image data for prompt: {Prompt}", request.Prompt);
                        return Problem("Failed to process image data after generation.", statusCode: StatusCodes.Status500InternalServerError);
                    }

                    _logger.LogInformation("Successfully generated image for prompt: {Prompt}. Returning image file.", request.Prompt);
                    imageStream.Position = 0; // Ensure stream is ready to be read
                    string downloadFileName = $"generated-image.{mimeType.Split('/').LastOrDefault() ?? \"png\"}";
                    return File(imageStream, mimeType, downloadFileName);
                }
            }
            catch (GeminiApiException ex)
            {
                _logger.LogError(ex, "API error during image generation for prompt: {Prompt}", request.Prompt);
                return StatusCode((int)ex.StatusCode, ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception during image generation for prompt: {Prompt}", request.Prompt);
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
        }
    }
}
```