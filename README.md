# GeminiSharp

GeminiSharp is a C# client SDK for seamlessly interacting with Google's Gemini API, enabling integration of Gemini's powerful text generation, image generation, and future capabilities into your .NET applications. With a simple, flexible, and robust interface, you can effortlessly generate content using the Gemini models.

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

---

## Current Status

GeminiSharp initially focused on text generation. Now, it supports:

- **Structured Output** ✅
- **Image Generation** ✅
- **Logging Support** ✅
- **Retry Configuration** ✅
- **Vision Support** 📷 _(coming soon)_
- **Audio Understanding** 🎧 _(coming soon)_
- **Code Execution** 💻 _(coming soon)_
- **Document Processing** 📄 _(coming soon)_

Stay tuned for more features! 🚀

---

## Installation

To install GeminiSharp via NuGet, run the following command in your project directory:

```bash
dotnet add package GeminiSharp
```

---

## Supported .NET Versions

| .NET Version | Supported |
|--------------|-----------|
| .NET 6       | ✅ Yes    |
| .NET 7       | ✅ Yes    |
| .NET 8       | ✅ Yes    |

---

## Usage

### Basic Example

```csharp
using System;
using GeminiSharp.Client;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var httpClient = new HttpClient();
        var apiKey = "your-gemini-api-key"; // Replace with your actual API key
        var geminiClient = new GeminiClient(httpClient, apiKey);

        try
        {
            var response = await geminiClient.GenerateContentAsync("gemini-1.5-flash", "Hello, Gemini! What's Falcon 9?");
            Console.WriteLine(response?.Candidates?[0].Content.Parts[0].Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### Structured Output Example

Generate structured responses using a custom schema:

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using System.Text.Json;

var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();
var response = await geminiClient.GenerateStructuredContentAsync<PlayerStats>(
    "gemini-2.0", "Provide cricket player stats for Virat Kohli", schema);
Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
```

### Image Generation Example

To generate images from prompts, see [Image Generation Documentation](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/image-generation.md).

---

## Logging

GeminiSharp uses **Serilog** for logging. To configure logging, add this to your `Program.cs`:

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting application");
```

For full logging setup details, refer to the [Logging Configuration Guide](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/logging.md).

---

## Retry Configuration

GeminiSharp includes a flexible retry configuration for handling transient errors. For detailed information on how to configure retries, check the [Retry Configuration Guide](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/retry-configuration.md).

---

## API Error Handling

GeminiSharp throws `GeminiApiException` for API errors. Here's an example of how to catch and inspect errors:

```csharp
try
{
    using var httpClient = new HttpClient();
    var geminiClient = new GeminiClient(httpClient, "your-gemini-api-key");

    var response = await geminiClient.GenerateContentAsync("invalid-model", "Test");
}
catch (GeminiApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}
```

---

## ASP.NET Core Example

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using Microsoft.AspNetCore.Mvc;

namespace GeminiSDKExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        public class GenerateTextRequest
        {
            public string Prompt { get; set; } = string.Empty;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateText(
            [FromBody] GenerateTextRequest request,
            [FromHeader(Name = "GeminiApiKey")] string apiKey,
            [FromHeader(Name = "Gemini-Model")] string? model)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest(new { error = "Prompt cannot be empty." });

            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest(new { error = "API key is required." });

            model ??= "gemini-1.5-flash"; // Default model

            using var httpClient = new HttpClient();
            var geminiClient = new GeminiClient(httpClient, apiKey);

            try
            {
                var result = await geminiClient.GenerateContentAsync(model, request.Prompt);
                return Ok(new { Response = result });
            }
            catch (GeminiApiException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
        }
    }
}
```

---

## Configuring the Base URL

You can easily configure the base URL for the Gemini API in your `GeminiClient`:

```csharp
using GeminiSharp.Client;

var customBaseUrl = "https://your-custom-gemini-api.com";
using var httpClient = new HttpClient();
var geminiClient = new GeminiClient(httpClient, "your-gemini-api-key", customBaseUrl);
```

---

## 📝 Notes

- **API Key Security**: Use secure methods for storing API keys such as environment variables, Azure Key Vault, AWS Secrets Manager, or .NET's User Secrets. Avoid hardcoding them.
- **HttpClient Management**: It is recommended to manage the `HttpClient` instance yourself. For example, you can use a single `HttpClient` instance for the entire application.
- **Error Handling**: Inspect the response object for errors even if no exception is thrown, as the API might still return error details.
- **Model Updates**: Check the official Gemini documentation for updates on models and new features.
- **Resource Management**: Ensure you properly dispose of `MemoryStream` objects and other resources to avoid memory leaks, especially in high-traffic environments like web APIs.
- **Logging**: Configure Serilog to capture detailed logs for debugging and troubleshooting.

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
# GeminiSharp

GeminiSharp is a C# client SDK for seamlessly interacting with Google's Gemini API, enabling integration of Gemini's powerful text generation, image generation, and future capabilities into your .NET applications. With a simple, flexible, and robust interface, you can effortlessly generate content using the Gemini models.

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

---

## Current Status

GeminiSharp initially focused on text generation. Now, it supports:

- **Structured Output** ✅
- **Image Generation** ✅
- **Logging Support** ✅
- **Retry Configuration** ✅
- **Vision Support** 📷 _(coming soon)_
- **Audio Understanding** 🎧 _(coming soon)_
- **Code Execution** 💻 _(coming soon)_
- **Document Processing** 📄 _(coming soon)_

Stay tuned for more features! 🚀

---

## Installation

To install GeminiSharp via NuGet, run the following command in your project directory:

```bash
dotnet add package GeminiSharp
```

---

## Supported .NET Versions

| .NET Version | Supported |
|--------------|-----------|
| .NET 6       | ✅ Yes    |
| .NET 7       | ✅ Yes    |
| .NET 8       | ✅ Yes    |

---

## Usage

### Basic Example

```csharp
using System;
using GeminiSharp.Client;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var httpClient = new HttpClient();
        var apiKey = "your-gemini-api-key"; // Replace with your actual API key
        var geminiClient = new GeminiClient(httpClient, apiKey);

        try
        {
            var response = await geminiClient.GenerateContentAsync("gemini-2.0", "Hello, Gemini! What's Falcon 9?");
            Console.WriteLine(response?.Candidates?[0].Content);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### Structured Output Example

Generate structured responses using a custom schema:

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using System.Text.Json;

var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();
var response = await geminiClient.GenerateStructuredContentAsync<PlayerStats>(
    "gemini-2.0", "Provide cricket player stats for Virat Kohli", schema);
Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
```

### Image Generation Example

To generate images from prompts, see [Image Generation Documentation](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/image-generation.md).

---

## Logging

GeminiSharp uses **Serilog** for logging. To configure logging, add this to your `Program.cs`:

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting application");
```

For full logging setup details, refer to the [Logging Configuration Guide](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/logging.md).

---

## Retry Configuration

GeminiSharp includes a flexible retry configuration for handling transient errors. For detailed information on how to configure retries, check the [Retry Configuration Guide](https://github.com/dprakash2101/GeminiSharp/blob/master/docs/retry-configuration.md).

---

## API Error Handling

GeminiSharp throws `GeminiApiException` for API errors. Here's an example of how to catch and inspect errors:

```csharp
try
{
    using var httpClient = new HttpClient();
    var geminiClient = new GeminiClient(httpClient, "your-gemini-api-key");

    var response = await geminiClient.GenerateContentAsync("invalid-model", "Test");
}
catch (GeminiApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}
```

---

## ASP.NET Core Example

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using Microsoft.AspNetCore.Mvc;

namespace GeminiSDKExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        public class GenerateTextRequest
        {
            public string Prompt { get; set; } = string.Empty;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateText(
            [FromBody] GenerateTextRequest request,
            [FromHeader(Name = "GeminiApiKey")] string apiKey,
            [FromHeader(Name = "Gemini-Model")] string? model)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest(new { error = "Prompt cannot be empty." });

            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest(new { error = "API key is required." });

            model ??= "gemini-1.5-flash"; // Default model

            using var httpClient = new HttpClient();
            var geminiClient = new GeminiClient(httpClient, apiKey);

            try
            {
                var result = await geminiClient.GenerateContentAsync(model, request.Prompt);
                return Ok(new { Response = result });
            }
            catch (GeminiApiException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
        }
    }
}
```

---

## Configuring the Base URL

You can easily configure the base URL for the Gemini API in your `GeminiClient`:

```csharp
using GeminiSharp.Client;

var customBaseUrl = "https://your-custom-gemini-api.com";
using var httpClient = new HttpClient();
var geminiClient = new GeminiClient(httpClient, "your-gemini-api-key", customBaseUrl);
```

---

## 📝 Notes

- **API Key Security**: Use secure methods for storing API keys such as environment variables, Azure Key Vault, AWS Secrets Manager, or .NET's User Secrets. Avoid hardcoding them.
- **Error Handling**: Inspect the response object for errors even if no exception is thrown, as the API might still return error details.
- **Model Updates**: Check the official Gemini documentation for updates on models and new features.
- **Resource Management**: Ensure you properly dispose of `MemoryStream` objects and other resources to avoid memory leaks, especially in high-traffic environments like web APIs.

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

---
