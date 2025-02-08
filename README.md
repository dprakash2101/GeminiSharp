# GeminiSharp

GeminiSharp is a C# client SDK for interacting with Google's Gemini API, enabling seamless integration of Gemini's powerful text generation capabilities into your .NET applications.  It provides a simple, flexible, and robust interface for generating content using Gemini models.

## Features

*   **Easy-to-use C# Client:**  Provides a straightforward API for interacting with Google's Gemini API from your .NET applications.
*   **Text Generation Focus:** Currently, GeminiSharp is optimized for text generation tasks.  Future releases will expand to support other Gemini functionalities.
*   **API Key Authentication:** Securely authenticate with the Gemini API using your API key.
*   **Configurable Model Selection:**  Easily specify the Gemini model to use for content generation.
*   **Configurable Base URL:** Customize the base URL for future flexibility and alternative endpoint support.
*   **Error Handling:**  Robustly handles API errors and exceptions, providing informative error messages.
*   **NuGet Package Support:**  Simple installation via NuGet package manager.

## Current Status

GeminiSharp currently focuses on text generation functionality.  Future development will include:

- **Vision support** 📷  
- **Audio understanding** 🎧  
- **Code execution** 💻  
- **Document processing** 📄  

Stay tuned for updates! 🚀 

## Installation

You can install GeminiSharp via NuGet:

```bash
dotnet add package GeminiSharp
```

## Usage
### Basic Example

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GeminiSharp.Client;

class Program
{
    static async Task Main()
    {
        using var httpClient = new HttpClient();
        var apiKey = "your-gemini-api-key"; // Replace with your actual API key
        var geminiClient = new GeminiClient(httpClient, apiKey);

        try
        {
            string model= "gemini-2.0-flash";
            var response = await geminiClient.GenerateContentAsync(model, "Hello, Gemini! ,What is Falcon 9?");
            Console.WriteLine(response?.Candidates?[0].Content);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### API Error Handling

The SDK throws GeminiApiException for API errors. You can catch and inspect the error details:

```csharp

try
{
    using var httpClient = new HttpClient();
    var apiKey = "your-gemini-api-key"; // Replace with your actual API key
    var geminiClient = new GeminiClient(httpClient, apiKey);

    var response = await geminiClient.GenerateContentAsync("invalid-model", "Test");
}
catch (GeminiApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}

```

### ASP.NET Core API Example

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using Microsoft.AspNetCore.Mvc;

namespace geminisdktest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiSDKController : ControllerBase
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

## Configuring the Base URL

You can configure the base URL for the Gemini API when creating the GeminiClient:

```csharp
using System.Net.Http;
using GeminiSharp.Client;

// Use a custom base URL.  Useful for testing or staging environments.
var customBaseUrl = "https://your-custom-gemini-api.com";
using var httpClient = new HttpClient();
var apiKey = "your-gemini-api-key"; // Replace with your actual API key
var geminiClient = new GeminiClient(httpClient, apiKey, customBaseUrl);
```



## Contributing

We welcome contributions! To get started, follow these steps:

1. **Fork** the repository.  
2. **Create** a new branch (`feature-branch-name`).  
3. **Make** your changes and **commit** with a clear message.  
4. **Push** your branch to your fork.  
5. **Open** a Pull Request (PR) and describe your changes.  

Feel free to discuss ideas or report issues in the [issues section](https://github.com/dprakash2101/GeminiSharp/issues).  
Thank you for contributing! 🚀  



## License

This project is licensed under the [MIT License](https://github.com/dprakash2101/GeminiSharp/blob/master/LICENSE).

## Author

**[Devi Prakash](https://github.com/dprakash2101)**
