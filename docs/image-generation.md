# 🖼️ Image Generation with GeminiSharp

This guide explains how to use the **GeminiSharp SDK** to generate images using Google's Gemini API, specifically targeting models designed for image creation like `gemini-2.0-flash-exp-image-generation`.

## ✨ Core Concept

Image generation involves sending a text prompt (a description of the desired image) to a compatible Gemini model via the SDK. The API processes the prompt and returns image data, typically encoded in Base64 format. The SDK provides clients and helpers to manage this process.

---

## 🔑 Key Components

*   **`GeminiImageGenerationClient`**: The primary client class for interacting with Gemini image generation endpoints. Requires your API key for initialization.
*   **`ImageGenerationConfig`**: A class to specify configuration options for the generation request. You might need to specify `ResponseModalities` depending on API requirements.
*   **`ImageConversionHelper.ConvertBase64ToImageStream`**: A utility function to convert the Base64 image data received from the API into a usable `MemoryStream`.
*   **Model**: You must use a model designated for image generation. As of this writing, `gemini-2.0-flash-exp-image-generation` is an example model for this purpose. *Note: Model availability and names can change; always refer to the official Google AI documentation for current models.*



## 🧑‍💻 C# Service Example

This example shows a reusable service class (`ImageGenerationService`) that encapsulates the logic for generating an image from a prompt and saving it to a file. It includes basic error handling and validation.

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Utilities; // Contains ImageGenerationConfig
using Microsoft.Extensions.Logging; // Using ILogger for structured logging
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ImageGenerationService
{
    private readonly ILogger<ImageGenerationService> _logger; // Optional: For logging
    private readonly string _apiKey;

    // Constructor accepting logger and API key
    public ImageGenerationService(ILogger<ImageGenerationService> logger, string apiKey)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }

    /// <summary>
    /// Generates an image based on the provided prompt using the Gemini API.
    /// </summary>
    /// <param name="prompt">The text description of the image to generate.</param>
    /// <returns>A MemoryStream containing the generated image data, or null if generation failed.</returns>
    public async Task<MemoryStream?> GenerateImageStreamAsync(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            _logger.LogError("Image generation prompt cannot be empty.");
            return null;
        }

        try
        {
            _logger.LogInformation("Initializing Gemini Image Generation Client.");
            var imageClient = new GeminiImageGenerationClient(_apiKey);

            // Define the model for image generation
            string model = "gemini-2.0-flash-exp-image-generation";
            _logger.LogInformation("Using model: {Model}", model);

            // Define configuration
            var config = new ImageGenerationConfig
            {
                 // Specify expected response types - consult API docs if needed
                 ResponseModalities = new List<string> { "Test","Image" }
                 // Add other config parameters like number of candidates if required
            };

            _logger.LogInformation("Sending image generation request for prompt: '{Prompt}'", prompt);
            var response = await imageClient.GenerateImageAsync(prompt, config, model);

            // Extract Base64 image data from the response
            // Structure: response -> Candidates -> Content -> Parts -> InlineData
            var imagePart = response?.Candidates?.FirstOrDefault()?
                                   .Content?.Parts?.FirstOrDefault(p => p.InlineData != null);
            var base64Image = imagePart?.InlineData?.Data;
            var mimeType = imagePart?.InlineData?.MimeType ?? "image/png"; // Default to PNG

            if (string.IsNullOrWhiteSpace(base64Image))
            {
                _logger.LogError("No image data found in the API response for prompt: '{Prompt}'. Full Response: {@Response}", prompt, response);
                return null;
            }

            _logger.LogInformation("Successfully received image data (MIME Type: {MimeType}). Converting from Base64.", mimeType);

            // Convert Base64 string to MemoryStream
            var imageStream = ImageConversionHelper.ConvertBase64ToImageStream(base64Image, mimeType);

            if (imageStream == null)
            {
                _logger.LogError("Failed to convert Base64 image data to stream for prompt: '{Prompt}'", prompt);
                return null;
            }

            _logger.LogInformation("Image stream created successfully for prompt: '{Prompt}'", prompt);
            imageStream.Position = 0; // Reset stream position before returning
            return imageStream;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating image for prompt: '{Prompt}'", prompt);
            return null; // Indicate failure
        }
    }

    /// <summary>
    /// Generates an image and saves it directly to a file.
    /// </summary>
    /// <param name="prompt">The text description of the image.</param>
    /// <param name="filePath">The path where the image file should be saved.</param>
    /// <returns>True if the image was saved successfully, false otherwise.</returns>
    public async Task<bool> GenerateAndSaveImageAsync(string prompt, string filePath)
    {
        var imageStream = await GenerateImageStreamAsync(prompt);

        if (imageStream == null)
        {
            _logger.LogError("Image generation failed for prompt: '{Prompt}', cannot save file.", prompt);
            return false;
        }

        try
        {
            _logger.LogInformation("Saving generated image to file: {FilePath}", filePath);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await imageStream.CopyToAsync(fileStream);
            }
            _logger.LogInformation("Image successfully saved to {FilePath}", filePath);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save image stream to file {FilePath}", filePath);
            return false;
        }
        finally
        {
            imageStream?.Dispose(); // Dispose the memory stream
        }
    }
}
```

### Using the Service

```csharp
// Assuming you have dependency injection setup for ILogger and configured API key access
// private readonly ImageGenerationService _imageGenerationService;

public async Task ExampleUsage(string userPrompt)
{
    // Option 1: Get the image stream (e.g., to return in an API response)
    var stream = await _imageGenerationService.GenerateImageStreamAsync(userPrompt);
    if (stream != null)
    {
        // Use the stream (e.g., return File(stream, "image/png"))
        // Remember to dispose the stream afterwards if not handled by the framework
        // await stream.DisposeAsync();
    }

    // Option 2: Save directly to a file
    string fileName = $"generated_{Guid.NewGuid()}.png";
    bool success = await _imageGenerationService.GenerateAndSaveImageAsync(userPrompt, fileName);
    if (success)
    {
        Console.WriteLine($"Image saved as {fileName}");
    }
    else
    {
        Console.WriteLine("Failed to generate or save the image.");
    }
}
```


## 🌐 .NET Web API Integration Example

This example demonstrates how to integrate the `GeminiImageGenerationClient` directly into an ASP.NET Core controller endpoint. The API key is passed via a header for simplicity (in production, use secure configuration methods).

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Utilities; // Contains ImageGenerationConfig
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;   // Required for StatusCodes
using Microsoft.Extensions.Logging; // Optional: For logging in the controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiSdkWebApi.Controllers // Adjust namespace
{
    // DTO for the request body
    public class GenerateImageApiRequest
    {
        public string Prompt { get; set; } = string.Empty;
    }

    [Route("api/imagegen")] // Example route
    [ApiController]
    public class GeminiImageGenerationController : ControllerBase
    {
        private readonly ILogger<GeminiImageGenerationController> _logger;

        public GeminiImageGenerationController(ILogger<GeminiImageGenerationController> logger)
        {
            _logger = logger; // Injected logger
        }

        [HttpPost("generate")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateImage(
            [FromBody] GenerateImageApiRequest request,
            [FromHeader(Name = "X-Gemini-ApiKey")] string apiKey) // API Key from header (use secure config in production!)
        {
            // --- Validation ---
            if (string.IsNullOrWhiteSpace(request?.Prompt))
            {
                _logger.LogWarning("GenerateImage API called with empty prompt.");
                return BadRequest("Prompt cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogWarning("GenerateImage API called without X-Gemini-ApiKey header.");
                // Use ProblemDetails for better error responses
                return Problem("X-Gemini-ApiKey header is required.", statusCode: StatusCodes.Status400BadRequest);
            }

            // --- Image Generation Logic ---
            try
            {
                _logger.LogInformation("Received image generation request. Prompt: '{Prompt}'", request.Prompt);

                var imageClient = new GeminiImageGenerationClient(apiKey);
                string model = "gemini-2.0-flash-exp-image-generation"; // Specify the image model

                var config = new ImageGenerationConfig
                {
                    ResponseModalities = new List<string> { "Image" } // Ensure image output
                };

                var response = await imageClient.GenerateImageAsync(request.Prompt, config, model);

                // Extract image data
                var imagePart = response?.Candidates?.FirstOrDefault()?
                                       .Content?.Parts?.FirstOrDefault(p => p.InlineData != null);
                var base64Image = imagePart?.InlineData?.Data;
                var mimeType = imagePart?.InlineData?.MimeType ?? "image/png"; // Default MIME type

                if (string.IsNullOrWhiteSpace(base64Image))
                {
                    _logger.LogError("No image data found in API response for prompt: '{Prompt}'. Response: {@Response}", request.Prompt, response);
                    // Return a suitable error - maybe the response contains details?
                    return Problem("Failed to generate image: No image data received from API.", statusCode: StatusCodes.Status502BadGateway);
                }

                _logger.LogInformation("Image data received (MIME Type: {MimeType}), converting from Base64.", mimeType);
                var imageStream = ImageConversionHelper.ConvertBase64ToImageStream(base64Image, mimeType);

                if (imageStream == null)
                {
                    _logger.LogError("Failed to convert Base64 image data for prompt: '{Prompt}'", request.Prompt);
                    return Problem("Failed to process image data after generation.", statusCode: StatusCodes.Status500InternalServerError);
                }

                _logger.LogInformation("Successfully generated image for prompt: '{Prompt}'. Returning image file.", request.Prompt);

                imageStream.Position = 0; // Ensure stream is ready to be read
                string downloadFileName = $"generated-image.{mimeType.Split('/').LastOrDefault() ?? "png"}";
                // Return the image stream as a file download
                return File(imageStream, mimeType, downloadFileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception during image generation for prompt: '{Prompt}'", request.Prompt);
                // Return a generic server error
                return Problem($"An unexpected error occurred while generating the image: {ex.Message}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
```

