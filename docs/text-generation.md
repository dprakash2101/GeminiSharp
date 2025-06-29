# Text Generation with GeminiSharp

GeminiSharp makes it easy to generate text using the Google Gemini API. This guide covers the basics of generating text, both with and without dependency injection.

## Using Dependency Injection

The recommended way to use GeminiSharp is by registering it with your service container.

### 1. Configuration

In your `Program.cs` or `Startup.cs`, add the following:

```csharp
using GeminiSharp.Extensions;

builder.Services.AddGeminiClient(options =>
{
    options.ApiKey = "YOUR_API_KEY";
    options.Model = "gemini-1.5-flash";
});
```

### 2. Generating Text

Inject the `GeminiClient` into your service or controller and use the `GenerateContentAsync` method:

```csharp
using GeminiSharp.Client;
using System.Threading.Tasks;

public class MyService
{
    private readonly GeminiClient _geminiClient;

    public MyService(GeminiClient geminiClient)
    {
        _geminiClient = geminiClient;
    }

    public async Task<string> GenerateTextAsync(string prompt)
    {
        var response = await _geminiClient.GenerateContentAsync(prompt);
        return response.Candidates[0].Content.Parts[0].Text;
    }
}
```

## Without Dependency Injection

If you prefer not to use dependency injection, you can instantiate the `GeminiClient` directly.

### 1. Initialization

Create a new instance of `GeminiClient`, providing the API key and model directly.

```csharp
using GeminiSharp.Client;
using GeminiSharp.Models.Configuration;

var geminiClient = new GeminiClient(
    new GeminiClientOptions
    {
        ApiKey = "YOUR_API_KEY",
        Model = "gemini-1.5-flash"
    },
    new HttpClient()
);
```

### 2. Generating Text

You can then use the `GenerateContentAsync` method as before:

```csharp
using System.Threading.Tasks;

public class MyService
{
    public async Task<string> GenerateTextAsync(string prompt)
    {
        var geminiClient = new GeminiClient(
            new GeminiClientOptions
            {
                ApiKey = "YOUR_API_KEY",
                Model = "gemini-1.5-flash"
            },
            new HttpClient()
        );

        var response = await geminiClient.GenerateContentAsync(prompt);
        return response.Candidates[0].Content.Parts[0].Text;
    }
}
```
