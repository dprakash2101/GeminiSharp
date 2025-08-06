# GeminiSharp

## Important Architectural Change: Re-architecting the SDK

We are currently undergoing a significant re-architecture of the GeminiSharp SDK to improve maintainability, modularity, and extensibility. The primary change involves breaking down the monolithic `GeminiClient` and previously separate clients like `GeminiStructuredResponseClient` into smaller, specialized client classes, each responsible for a specific area of Gemini API functionality (e.g., `TextClient`, `ImageClient`, `DocumentClient`, `AudioClient`, `VideoClient`, `StructuredContentClient`, `UtilityClient`).

**Key Changes:**
- **Specialized Clients:** Functionality previously bundled in `GeminiClient` and other specific clients has been moved to dedicated client classes (e.g., `TextClient` for text generation, `ImageClient` for image-related operations).
- **Facade Pattern:** The main `GeminiClient` now acts as a facade, instantiating and delegating calls to these specialized clients. This maintains a consistent public API while allowing for internal modularity.
- **Default Model Update:** Where applicable, the default Gemini model has been updated to `gemini-2.5-flash` for improved performance and access to the latest features.
- **Code Execution Feature (Temporarily Removed):** The code execution feature has been temporarily removed during this re-architecture phase to streamline the process. It will be re-introduced in a future update.

This re-architecture aims to make the SDK easier to understand, use, and contribute to, while also paving the way for more robust and feature-rich implementations in the future.

---

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

GeminiSharp now provides a comprehensive set of specialized clients for various Gemini API functionalities:

- **Text Generation** ✅
- **Image Generation** ✅
- **Structured Output** ✅
- **Chat Sessions** ✅
- **Embeddings** ✅
- **Token Counting** ✅
- **Model Information Retrieval** ✅
- **Document Processing** ✅
- **Audio Processing** ✅
- **Video Processing** ✅
- **Function Calling** ✅

We are continuously working to expand functionality and improve the SDK. Stay tuned for more features! 🚀

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

## Dependency Injection Setup

GeminiSharp is designed for seamless integration with .NET's built-in Dependency Injection (DI) system. This allows for easy management of `IGeminiClient` instances and their dependencies, suchs as `HttpClient` and retry policies.

### Basic DI Setup

To register GeminiSharp services with your `IServiceCollection`, use the `AddGeminiSharp` extension method. This is typically done in your `Startup.cs` (for ASP.NET Core) or `Program.cs` (for .NET 6+ minimal APIs/Console apps).

```csharp
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using GeminiSharp.Models.Utilities; // Required for GeminiSharpOptions
using System;
using System.Collections.Generic;

// In your Startup.cs ConfigureServices method or Program.cs
public void ConfigureServices(IServiceCollection services)
{
    // ... other service registrations

    services.AddGeminiSharp(options =>
    {
        options.ApiKey = "YOUR_GEMINI_API_KEY"; // REQUIRED: Replace with your actual API key

        // Optional: Configure BaseUrl if not using the default Google endpoint
        // options.BaseUrl = "https://your-custom-gemini-api.com/v1beta/models/";

        // Optional: Configure retry policy
        options.RetryConfig = new RetryConfig
        {
            MaxRetries = 5, // Example: Retry up to 5 times
            InitialDelayMs = 2000, // Example: 2-second initial delay
            UseExponentialBackoff = true, // Example: Use exponential backoff
            RetryStatusCodes = new HashSet<int> { 429, 500, 503, 504 } // Example: Add 504 Gateway Timeout
        };
    });

    // ... other service registrations
}

// Or for .NET 6+ minimal APIs/Console apps:
var builder = WebApplication.CreateBuilder(args);
// ...
builder.Services.AddGeminiSharp(options =>
{
    options.ApiKey = "YOUR_GEMINI_API_KEY"; // REQUIRED: Replace with your actual API key

    // Optional: Configure BaseUrl if not using the default Google endpoint
    // options.BaseUrl = "https://your-custom-gemini-api.com/v1beta/models/";

    // Optional: Configure retry policy
    options.RetryConfig = new RetryConfig
    {
        MaxRetries = 5,
        InitialDelayMs = 2000,
        UseExponentialBackoff = true,
        RetryStatusCodes = new HashSet<int> { 429, 500, 503, 504 }
    };
});
// ...
```

---

## Usage

Once registered, you can inject `IGeminiClient` into your classes (e.g., controllers, services, or console application entry points) and use it to interact with the Gemini API.

### Console Application Example (with DI)

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection; // Required for DI
using Serilog; // For logging example
using Microsoft.Extensions.Logging; // For ILogger

class Program
{
    static async Task Main(string[] args)
    {
        // Configure Serilog (optional, but recommended for visibility)
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        // 1. Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ILogger>(Log.Logger) // Register Serilog logger
            .AddGeminiSharp(options =>
            {
                options.ApiKey = "YOUR_GEMINI_API_KEY"; // REQUIRED: Replace with your actual API key
                // Optional: Configure BaseUrl or RetryConfig here if needed
                // options.BaseUrl = "https://your-custom-gemini-api.com/v1beta/models/";
            })
            .BuildServiceProvider();

        // 2. Resolve IGeminiClient from the container
        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        try
        {
            // Using the TextClient via GeminiClient facade
            Console.WriteLine("Sending request to Gemini...");
            var response = await geminiClient.GenerateTextAsync(null, "Hello, Gemini! What's Falcon 9?");
            Console.WriteLine("Response from Gemini:");
            Console.WriteLine(response?.Candidates?[0].Content?.Parts?[0].Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            Console.WriteLine($"Status Code: {ex.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            Log.CloseAndFlush(); // Ensure all logs are written
        }
    }
}
```

### Structured Output Example

Generate structured responses using a custom schema:

```csharp
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

// Define a simple class to represent the structured output
public class PlayerStats
{
    public string? Name { get; set; }
    public string? Team { get; set; }
    public int Runs { get; set; }
    public double BattingAverage { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(options => options.ApiKey = "YOUR_GEMINI_API_KEY") // Replace with your actual API key
            .BuildServiceProvider();

        // 2. Resolve IGeminiClient from the container
        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        // 3. Generate the JSON schema from your C# model
        var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();

        try
        {
            // 4. Call GenerateStructuredContentAsync using the GeminiClient facade
            var response = await geminiClient.GenerateStructuredContentAsync(
                null, // Use default model (gemini-2.5-flash)
                "Provide cricket player stats for Virat Kohli",
                schema,
                new GeminiSharp.Models.Request.GenerationConfig { response_mime_type = "application/json" } // Ensure JSON output
            );

            // 5. Extract and deserialize the JSON string from the response
            var jsonText = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            if (!string.IsNullOrWhiteSpace(jsonText))
            {
                PlayerStats? structuredData = JsonSerializer.Deserialize<PlayerStats>(jsonText, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });
                Console.WriteLine(JsonSerializer.Serialize(structuredData, new JsonSerializerOptions { WriteIndented = true }));
            }
            else
            {
                Console.WriteLine("Error: No structured content received from the API.");
            }
        }
        catch (GeminiSharp.API.GeminiApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}


### Image Generation Example

To generate images from prompts, see [Image Generation Documentation](docs/image-generation.md).

### Chat Sessions Example

For interactive multi-turn conversations, see [Chat Sessions Documentation](docs/chat-sessions.md).

### Document Processing Example

To process documents (PDFs, etc.), see [Document Processing Documentation](docs/document-processing.md).

### Video Processing Example

To process video content, see [Video Processing Documentation](docs/video-processing.md).

### Audio Processing Example

To process audio content, see [Audio Processing Documentation](docs/audio-processing.md).

### Utility Client Example

For embeddings, token counting, and model information, see [Utility Client Documentation](docs/utility-client.md).

### Function Calling Example

To enable the model to call external tools/functions, see [Function Calling Documentation](docs/function-calling.md).

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

For full logging setup details, refer to the [Logging Configuration Guide](docs/logging-configuration.md).

---

## Retry Configuration

GeminiSharp includes a flexible retry configuration for handling transient errors. For detailed information on how to configure retries, check the [Retry Configuration Guide](docs/retry-configuration.md).

---

## API Error Handling

GeminiSharp throws `GeminiApiException` for API errors. Here's an example of how to catch and inspect errors:

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(options => options.ApiKey = "YOUR_GEMINI_API_KEY")
            .BuildServiceProvider();

        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        try
        {
            var response = await geminiClient.GenerateTextAsync("invalid-model", "Test");
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            Console.WriteLine($"Status Code: {ex.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
```

---

## ASP.NET Core Example

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using GeminiSharp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection; // Required for DI
using System.Threading.Tasks;

namespace GeminiSDKExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiController : ControllerBase
    {
        private readonly IGeminiClient _geminiClient;

        // Inject IGeminiClient via constructor
        public GeminiController(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }

        public class GenerateTextRequest
        {
            public string Prompt { get; set; } = string.Empty;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateText(
            [FromBody] GenerateTextRequest request,
            [FromHeader(Name = "Gemini-Model")] string? model)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest(new { error = "Prompt cannot be empty." });

            // API Key is configured via DI, no longer needed in header for this example
            // If you need per-request API keys, you'd handle it differently (e.g., custom HttpClientFactory)

            try
            {
                var result = await _geminiClient.GenerateTextAsync(model, request.Prompt);
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

You can configure the base URL for the Gemini API during DI setup:

```csharp
using GeminiSharp.Extensions;
using GeminiSharp.Models.Utilities;
using Microsoft.Extensions.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
    services.AddGeminiSharp(options =>
    {
        options.ApiKey = "YOUR_GEMINI_API_KEY";
        options.BaseUrl = "https://your-custom-gemini-api.com/v1beta/models/";
    });
}
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