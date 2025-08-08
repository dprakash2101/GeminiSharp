# 🚀 GeminiSharp: C# SDK for Google Gemini API

[![NuGet](https://img.shields.io/nuget/v/GeminiSharp)](https://www.nuget.org/packages/GeminiSharp/)
[![License](https://img.shields.io/github/license/dprakash2101/GeminiSharp)](https://github.com/dprakash2101/GeminiSharp/blob/master/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/dprakash2101/GeminiSharp)](https://github.com/dprakash2101/GeminiSharp/issues)
[![GitHub stars](https://img.shields.io/github/stars/dprakash2101/GeminiSharp)](https://github.com/dprakash2101/GeminiSharp/stargazers)

---

**GeminiSharp** is a powerful and intuitive C# client SDK for the Google Gemini API. It provides a seamless way to integrate Gemini's cutting-edge text and image generation capabilities into your .NET applications. With a focus on simplicity, flexibility, and robustness, GeminiSharp makes it effortless to generate content with the latest Gemini models.

---

## ✨ Features

- **🤖 Easy-to-use C# Client:** A developer-friendly API for interacting with the Gemini API in your .NET projects.
- **📝 Text & Structured Output:** Supports both free-form text generation and structured output using JSON schemas.
- **🖼️ Image Generation:** Generate stunning images from text prompts with the Gemini API.
- **✍️ Logging Support:** Integrated with Serilog for comprehensive logging of internal SDK activities.
- **🔁 Retry Configuration:** Built-in support for customizable retry policies to handle transient API failures.
- **🔑 API Key Authentication:** Securely authenticate using your Gemini API key.
- **⚙️ Configurable Models:** Easily select the Gemini model that best suits your needs.
- **🌐 Customizable Base URL:** Flexibility to change the base URL for API endpoints.
- **🚨 Robust Error Handling:** Detailed error messages and exceptions for easier debugging.
- **📦 NuGet Package:** Simple integration into your project via NuGet.

---

##  Architectural Rearchitecture & DI Improvements

GeminiSharp has undergone a significant architectural refactoring to improve dependency injection (DI), maintainability, and the overall developer experience. The SDK is now more modular and easier to configure, especially for modern .NET applications.

### Key Improvements:

- **🚀 Simplified DI:** The `AddGeminiSharp` extension method now uses the `IOptions<GeminiSharpOptions>` pattern, making it easier to configure the client from `appsettings.json` or in your code.
- **🔧 Modular Clients:** The monolithic `GeminiClient` has been broken down into smaller, specialized clients (`TextClient`, `ImageClient`, etc.), which are now managed internally.
- **🌐 Centralized HttpClient:** The `HttpClient` is now managed by the `IHttpClientFactory`, ensuring best practices for handling HTTP connections.
- **🔁 Enhanced Retry Logic:** The Polly retry logic is now integrated directly into the `HttpClient` pipeline, providing a more robust and configurable retry mechanism.

These changes make GeminiSharp more aligned with modern .NET development practices and provide a more seamless integration experience.

---

## 📦 Installation

Install GeminiSharp via NuGet:

```bash
dotnet add package GeminiSharp
```

---

## ⚙️ Dependency Injection Setup

GeminiSharp is designed for easy integration with .NET's DI system.

### Basic DI Setup

Register GeminiSharp services in your `Startup.cs` or `Program.cs`:

```csharp
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using GeminiSharp.Models.Utilities;

public void ConfigureServices(IServiceCollection services)
{
    services.AddGeminiSharp(options =>
    {
        options.ApiKey = "YOUR_GEMINI_API_KEY";
        options.RetryConfig = new RetryConfig
        {
            MaxRetries = 5,
            InitialDelayMs = 2000,
            UseExponentialBackoff = true,
            RetryStatusCodes = new HashSet<int> { 429, 500, 503, 504 }
        };
    });
}
```

---

## 🚀 Usage

Inject `IGeminiClient` into your services and start making API calls.

### Console App Example

```csharp
using GeminiSharp.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(options =>
            {
                options.ApiKey = "YOUR_GEMINI_API_KEY";
            })
            .BuildServiceProvider();

        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        try
        {
            var response = await geminiClient.GenerateTextAsync("gemini-1.5-flash", "Hello, Gemini!");
            Console.WriteLine(response?.Candidates?[0].Content?.Parts?[0].Text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

---

## 📚 Documentation

For more detailed information, check out the documentation:

- [Image Generation](docs/image-generation.md)
- [Chat Sessions](docs/chat-sessions.md)
- [Document Processing](docs/document-processing.md)
- [Video Processing](docs/video-processing.md)
- [Audio Processing](docs/audio-processing.md)
- [Utility Client](docs/utility-client.md)
- [Function Calling](docs/function-calling.md)
- [Logging](docs/logging.md)
- [Retry Configuration](docs/retry-configuration.md)

---

## 🤝 Contributing

Contributions are welcome! Please fork the repository and open a pull request.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## ✍️ Author

**[Devi Prakash](https://github.com/dprakash2101)**
