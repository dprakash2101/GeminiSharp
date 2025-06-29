# GeminiSharp

GeminiSharp is a C# client SDK for seamlessly interacting with Google's Gemini API, enabling integration of Gemini's powerful text generation, image generation, and future capabilities into your .NET applications. With a simple, flexible, and robust interface, you can effortlessly generate content using the Gemini models.

---

## 🚀 Getting Started

### 1. Installation

To install GeminiSharp via NuGet, run the following command in your project directory:

```bash
dotnet add package GeminiSharp
```

### 2. Configuration

The recommended way to use GeminiSharp is with dependency injection. In your `Program.cs` or `Startup.cs`, configure the Gemini clients as follows:

```csharp
using GeminiSharp.Extensions;

builder.Services.AddGeminiClient(options =>
{
    options.ApiKey = builder.Configuration["GeminiApiKey"];
    options.Model = "gemini-1.5-flash"; // Or any other suitable model
});
```

### 3. Usage

Inject the desired client into your services or controllers. GeminiSharp provides the following clients:

*   `GeminiClient`: For general text-based content generation.
*   `GeminiImageGenerationClient`: For generating images from text prompts.
*   `GeminiStructuredClient`: For generating structured data (e.g., JSON) from prompts.
*   `GeminiDocumentUnderstandingClient`: For processing and understanding uploaded documents.

#### Example: Using `GeminiClient`

```csharp
using GeminiSharp.Client;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
    private readonly GeminiClient _geminiClient;

    public MyController(GeminiClient geminiClient)
    {
        _geminiClient = geminiClient;
    }

    [HttpGet("generate")]
    public async Task<IActionResult> GenerateText(string prompt)
    {
        var response = await _geminiClient.GenerateContentAsync(prompt);
        return Ok(response);
    }
}
```

### Using without Dependency Injection

If you prefer not to use dependency injection, you can instantiate the clients directly. You'll need to create an `HttpClient` and a `GeminiApiClient` instance first.

```csharp
using GeminiSharp.API;
using GeminiSharp.Client;
using GeminiSharp.Models.Configuration;
using System.Net.Http;

// 1. Configure and create an HttpClient
var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/"),
};
httpClient.DefaultRequestHeaders.Add("x-goog-api-key", "YOUR_API_KEY");

// 2. Create the GeminiApiClient
var apiClient = new GeminiApiClient(httpClient, "gemini-1.5-flash");

// 3. Instantiate the desired client
var geminiClient = new GeminiClient(apiClient);

// 4. Use the client to generate content
var response = await geminiClient.GenerateContentAsync("your prompt");
```

---

## 📚 Documentation

For more detailed information on how to use GeminiSharp, please refer to the following documentation:

*   [Text Generation](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/text-generation.md)
*   [Image Generation](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/image-generation.md)
*   [Logging](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/logging.md)
*   [Retry Configuration](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/retry-configuration.md)
*   [Structured Output](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/structured-output.md)
*   [Document Understanding](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/document-understanding.md)

---

## Features

- **Easy-to-use C# Client:** A straightforward API for interacting with the Gemini API in your .NET applications.
- **Text Generation & Structured Output:** Supports both free-form text generation and structured output based on user-defined JSON schema.
- **Image Generation Support:** Generate images from text prompts using the Gemini API.
- **Logging Support:** Integrated with Serilog (or other logging frameworks) for capturing internal SDK logs.
- **Retry Configuration:** Built-in support for retry policies to handle transient failures.
- **API Key Authentication:** Secure authentication using your Gemini API key.
- **Configurable Model Selection:** Easily specify which Gemini model to use for content generation.
- **Customizable Base URL:** Allows you to change the base URL, ideal for future flexibility and alternative endpoint support.
- **Error Handling:** Robust error handling with detailed messages and exceptions.
- **NuGet Package Support:** Install via NuGet for simple integration into your project.
- **Cancellation Support:** Supports `CancellationToken` for asynchronous operations.

---

## Current Status

GeminiSharp initially focused on text generation. Now, it supports:

- **Structured Output** ✅
- **Image Generation** ✅
- **Logging Support** ✅
- **Retry Configuration** ✅
- **Document Understanding** ✅
- **Vision Support** 📷 _(coming soon)_
- **Audio Understanding** 🎧 _(coming soon)_
- **Code Execution** 💻 _(coming soon)_

Stay tuned for more features! 🚀

---

## Supported .NET Versions

| .NET Version | Supported |
|--------------|-----------|
| .NET 6       | ✅ Yes    |
| .NET 7       | ✅ Yes    |
| .NET 8       | ✅ Yes    |

---

## Contributing

We welcome contributions! To get started:

1. **Fork** the repository.
2. **Create** a new branch (`feature-branch-name`).
3. **Make** your changes and **commit** them.
4. **Push** your branch to your fork.
5. **Open** a Pull Request (PR) with a clear description of your changes.

Visit the [issues section](https://github.com/dprakash2101/GeminiSharp/issues) to discuss ideas or report issues.

---

## License

This project is licensed under the [MIT License](https://github.com/dprakash2101/GeminiSharp/blob/master/LICENSE).

---

## Author

**[Devi Prakash](https://github.com/dprakash2101)**
