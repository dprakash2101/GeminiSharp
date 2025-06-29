# 🖼️ Image Generation with GeminiSharp

This guide provides a comprehensive overview of how to generate images using the **GeminiSharp SDK**. You'll learn how to set up the client, make requests, and process the image data returned by the API.

## ✨ Core Concepts

Image generation with Gemini involves sending a text prompt—a description of the image you want to create—to a compatible Gemini model. The API processes this prompt and returns the image data, which is typically encoded in Base64 format. GeminiSharp simplifies this process by providing dedicated clients and helper methods.

---

## 🔑 Key Components

*   **`GeminiImageGenerationClient`**: The primary client for interacting with Gemini's image generation endpoints. It handles the communication with the API and requires your API key for authentication.
*   **`ImageGenerationConfig`**: A class used to configure the image generation request. You can specify parameters like the number of images to generate and the expected response format.
*   **`ImageConversionHelper.ConvertBase64ToImageStream`**: A utility function that converts the Base64-encoded image data received from the API into a `MemoryStream`, which can then be easily saved to a file or used in your application.
*   **Model**: You must use a model that supports image generation. The model names can change, so always refer to the official Google AI documentation for the latest information.

---

## 🚀 Getting Started

### 1. Installation

First, make sure you have the GeminiSharp NuGet package installed in your project:

```bash
dotnet add package GeminiSharp
```

### 2. Setting up the Client

The recommended way to use the `GeminiImageGenerationClient` is by registering it with the dependency injection container in your `Program.cs` or `Startup.cs`:

```csharp
using GeminiSharp.Extensions;

builder.Services.AddGeminiClient(options =>
{
    options.ApiKey = builder.Configuration["GeminiApiKey"];
});
```

This will make the `GeminiImageGenerationClient` available throughout your application.

### 3. Injecting the Client

You can then inject the `GeminiImageGenerationClient` into your services or controllers:

```csharp
public class ImageService
{
    private readonly GeminiImageGenerationClient _imageClient;

    public ImageService(GeminiImageGenerationClient imageClient)
    {
        _imageClient = imageClient;
    }

    // ... your image generation logic
}
```

---

## 🧑‍💻 Code Examples

### Generating an Image and Saving it to a File

This example demonstrates how to generate an image from a prompt and save it to a file.

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Models.Request;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ImageGenerationService
{
    private readonly GeminiImageGenerationClient _imageClient;

    public ImageGenerationService(GeminiImageGenerationClient imageClient)
    {
        _imageClient = imageClient;
    }

    public async Task<string> GenerateAndSaveImageAsync(string prompt, string outputPath)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            throw new ArgumentException("Prompt cannot be empty.", nameof(prompt));
        }

        var config = new ImageGenerationConfig
        {
            ResponseModalities = new List<string> { "Image" }
        };

        var response = await _imageClient.GenerateImageAsync(prompt, config, "gemini-1.5-flash-exp-image-generation");

        var imagePart = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault(p => p.InlineData != null);
        var base64Image = imagePart?.InlineData?.Data;
        var mimeType = imagePart?.InlineData?.MimeType ?? "image/png";

        if (string.IsNullOrWhiteSpace(base64Image))
        {
            throw new Exception("No image data found in the API response.");
        }

        var imageStream = ImageConversionHelper.ConvertBase64ToImageStream(base64Image, mimeType);
        var fileExtension = mimeType.Split('/').LastOrDefault() ?? "png";
        var fullPath = $"{outputPath}.{fileExtension}";

        using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await imageStream.CopyToAsync(fileStream);
        }

        return fullPath;
    }
}
```

### Integrating with an ASP.NET Core API

This example shows how to create an API endpoint that generates an image and returns it as a file stream.

```csharp
using GeminiSharp.Client;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ImageGenerationController : ControllerBase
{
    private readonly ImageGenerationService _imageGenerationService;

    public ImageGenerationController(ImageGenerationService imageGenerationService)
    {
        _imageGenerationService = imageGenerationService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateImage([FromBody] string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            return BadRequest("Prompt cannot be empty.");
        }

        try
        {
            var imagePath = await _imageGenerationService.GenerateAndSaveImageAsync(prompt, "generated_image");
            var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            return File(imageStream, "image/png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
```

---

## ⚠️ Error Handling

When working with image generation, it's important to handle potential errors gracefully. Here are some common scenarios to consider:

*   **Invalid API Key**: If the API key is invalid or missing, the request will fail with an authentication error.
*   **Invalid Model**: Using a model that doesn't support image generation will result in an error.
*   **No Image Data**: The API response may not contain any image data. This can happen if the prompt is too vague or violates the API's usage policies.
*   **Network Issues**: Transient network errors can cause the request to fail. The SDK's built-in retry mechanism can help mitigate this, but it's still important to have proper error handling in your code.

By implementing robust error handling, you can ensure that your application remains stable and provides a good user experience, even when things go wrong.