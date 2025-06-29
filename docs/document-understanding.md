# Document Understanding with GeminiSharp

GeminiSharp now supports document understanding, allowing you to ask questions and extract information from various file types, including PDFs, images, and more. This feature is available through the `GeminiDocumentUnderstandingClient`.

## Getting Started

To use the document understanding client, you first need to register it in your dependency injection container, as shown in the example below.

### Registration

```csharp
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
    services.AddGeminiClient(options =>
    {
        options.ApiKey = "YOUR_API_KEY";
        options.Model = "gemini-1.5-flash"; // Or any other suitable model
    });
}
```

### Basic Usage

Once registered, you can inject `GeminiDocumentUnderstandingClient` into your services and use it to ask questions about a document.

```csharp
using GeminiSharp.Client;
using System.Threading.Tasks;
using System.Threading;

public class DocumentService
{
    private readonly GeminiDocumentUnderstandingClient _documentClient;

    public DocumentService(GeminiDocumentUnderstandingClient documentClient)
    {
        _documentClient = documentClient;
    }

    public async Task<string> AskQuestionAsync(string filePath, string question, CancellationToken cancellationToken = default)
    {
        // The MIME type is optional and will be inferred if not provided
        var response = await _documentClient.GenerateContentAsync(filePath, question, cancellationToken: cancellationToken);
        return response;
    }
}
```

### Working with Multiple Documents

You can also ask a question about multiple documents at once. The client will process all documents and provide a consolidated answer.

```csharp
using GeminiSharp.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class MultiDocumentService
{
    private readonly GeminiDocumentUnderstandingClient _documentClient;

    public MultiDocumentService(GeminiDocumentUnderstandingClient documentClient)
    {
        _documentClient = documentClient;
    }

    public async Task<string> AskQuestionAsync(IEnumerable<string> filePaths, string question, CancellationToken cancellationToken = default)
    {
        var response = await _documentClient.GenerateContentAsync(filePaths, question, cancellationToken);
        return response;
    }
}
```

This new functionality makes it easier than ever to integrate powerful document analysis capabilities into your applications with GeminiSharp.
