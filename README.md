# GeminiSharp - the C# library for the Google Gemini API

Comprehensive API for interacting with Google's Gemini models supporting text, chat, image generation, file uploads, grounding, code execution, model tuning, and more.

## Frameworks supported

## Dependencies

- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 13.0.2 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.8.0 or later
- [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) - 5.0.0 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
Install-Package System.ComponentModel.Annotations
```

## Installation
```sh
dotnet add package GeminiSharp
```

### Connections
Each ApiClass (properly the ApiClient inside it) will create an instance of HttpClient. It will use that for the entire lifecycle and dispose it when called the Dispose method.

To better manager the connections it's a common practice to reuse the HttpClient and HttpClientHandler (see [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net) for details). To use your own HttpClient instance just pass it to the ApiClass constructor.

```csharp
HttpClientHandler yourHandler = new HttpClientHandler();
HttpClient yourHttpClient = new HttpClient(yourHandler);
var api = new YourApiClass(yourHttpClient, yourHandler);
```

If you want to use an HttpClient and don't have access to the handler, for example in a DI context in Asp.net Core when using IHttpClientFactory.

```csharp
HttpClient yourHttpClient = new HttpClient();
var api = new YourApiClass(yourHttpClient);
```
You'll loose some configuration settings, the features affected are: Setting and Retrieving Cookies, Client Certificates, Proxy settings. You need to either manually handle those in your setup of the HttpClient or they won't be available.

Here an example of DI setup in a sample web project:

```csharp
services.AddHttpClient<YourApiClass>(httpClient =>
   new PetApi(httpClient));
```


## Getting Started

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;
using Serilog;
using Serilog.Sinks.Console;

namespace Example
{
    public class Example
    {
        public static void Main()
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Set minimum logging level
                .WriteTo.Console()    // Output logs to console
                .CreateLogger();

            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Assign the logger to the configuration
            config.Logger = Log.Logger;
            // Configure API key authorization: ApiKeyHeader
            config.ApiKey.Add("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.ApiKeyPrefix.Add("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.ApiKey.Add("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.ApiKeyPrefix.Add("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = "model_example";  // string | 
            var batchEmbedContentsRequest = new BatchEmbedContentsRequest(); // BatchEmbedContentsRequest | 

            try
            {
                // Batch Embed Contents
                BatchEmbedContents200Response result = apiInstance.BatchEmbedContents(model, batchEmbedContentsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling GeminiApi.BatchEmbedContents: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
```

## Advanced Features

This client includes several helpers to facilitate common tasks.

### Retry Logic

The API client supports custom retry logic using the [Polly](https://github.com/App-vNext/Polly) library. You can define your own retry policies for handling transient network errors or other temporary issues.

For detailed instructions and examples, see the [Retry Logic Documentation](./docs/RetryLogic.md).

### Logging

The client is instrumented with `Serilog` to provide detailed logging of API requests and responses. This is useful for debugging and monitoring. Logging is opt-in and can be configured by providing a logger instance.

For more details, see the [Logging Documentation](./docs/Logging.md).

### JSON Schema Generation

A utility is provided to generate JSON schemas from your C# model classes. This is especially useful for defining tools and functions for the Gemini API.

For more information, see the [JSON Schema Generation Documentation](./docs/JsonSchemaGenerator.md).

### File to Base64 Conversion

The client includes a helper to easily convert files and streams to base64-encoded strings, which is useful for embedding file data in API requests.

For usage examples, see the [File to Base64 Conversion Documentation](./docs/FileConverter.md).


## Documentation for API Endpoints

All URIs are relative to *https://generativelanguage.googleapis.com*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*GeminiApi* | [**BatchEmbedContents**](docs/GeminiApi.md#batchembedcontents) | **POST** /v1/models/{model}:batchEmbedContents | Batch Embed Contents
*GeminiApi* | [**CancelOperation**](docs/GeminiApi.md#canceloperation) | **DELETE** /v1/operations/{name} | Cancel Operation
*GeminiApi* | [**CountTokens**](docs/GeminiApi.md#counttokens) | **POST** /v1/models/{model}:countTokens | Count Tokens
*GeminiApi* | [**CreateCachedContent**](docs/GeminiApi.md#createcachedcontent) | **POST** /v1/cachedContents | Create Cached Content
*GeminiApi* | [**CreateChunk**](docs/GeminiApi.md#createchunk) | **POST** /v1/corpora/{corpus}/documents/{document}/chunks | Create Chunk
*GeminiApi* | [**CreateCorpus**](docs/GeminiApi.md#createcorpus) | **POST** /v1/corpora | Create Corpus
*GeminiApi* | [**CreateDocument**](docs/GeminiApi.md#createdocument) | **POST** /v1/corpora/{corpus}/documents | Create Document
*GeminiApi* | [**CreateTunedModel**](docs/GeminiApi.md#createtunedmodel) | **POST** /v1/tunedModels | Create Tuned Model
*GeminiApi* | [**DeleteCachedContent**](docs/GeminiApi.md#deletecachedcontent) | **DELETE** /v1/cachedContents/{name} | Delete Cached Content
*GeminiApi* | [**DeleteChunk**](docs/GeminiApi.md#deletechunk) | **DELETE** /v1/corpora/{corpus}/documents/{document}/chunks/{chunk} | Delete Chunk
*GeminiApi* | [**DeleteCorpus**](docs/GeminiApi.md#deletecorpus) | **DELETE** /v1/corpora/{name} | Delete Corpus
*GeminiApi* | [**DeleteDocument**](docs/GeminiApi.md#deletedocument) | **DELETE** /v1/corpora/{corpus}/documents/{document} | Delete Document
*GeminiApi* | [**DeleteFile**](docs/GeminiApi.md#deletefile) | **DELETE** /v1/files/{name} | Delete File
*GeminiApi* | [**DeleteTunedModel**](docs/GeminiApi.md#deletetunedmodel) | **DELETE** /v1/tunedModels/{name} | Delete Tuned Model
*GeminiApi* | [**EmbedContent**](docs/GeminiApi.md#embedcontent) | **POST** /v1/models/{model}:embedContent | Embed Content
*GeminiApi* | [**GenerateContent**](docs/GeminiApi.md#generatecontent) | **POST** /v1/models/{model}:generateContent | Generate Content
*GeminiApi* | [**GenerateImage**](docs/GeminiApi.md#generateimage) | **POST** /v1/models/{model}:generateImage | Generate Image
*GeminiApi* | [**GetCachedContent**](docs/GeminiApi.md#getcachedcontent) | **GET** /v1/cachedContents/{name} | Get Cached Content
*GeminiApi* | [**GetChunk**](docs/GeminiApi.md#getchunk) | **GET** /v1/corpora/{corpus}/documents/{document}/chunks/{chunk} | Get Chunk
*GeminiApi* | [**GetCorpus**](docs/GeminiApi.md#getcorpus) | **GET** /v1/corpora/{name} | Get Corpus
*GeminiApi* | [**GetDocument**](docs/GeminiApi.md#getdocument) | **GET** /v1/corpora/{corpus}/documents/{document} | Get Document
*GeminiApi* | [**GetFile**](docs/GeminiApi.md#getfile) | **GET** /v1/files/{name} | Get File
*GeminiApi* | [**GetModel**](docs/GeminiApi.md#getmodel) | **GET** /v1/models/{model} | Get Model
*GeminiApi* | [**GetOperation**](docs/GeminiApi.md#getoperation) | **GET** /v1/operations/{name} | Get Operation
*GeminiApi* | [**GetTunedModel**](docs/GeminiApi.md#gettunedmodel) | **GET** /v1/tunedModels/{name} | Get Tuned Model
*GeminiApi* | [**ListCachedContents**](docs/GeminiApi.md#listcachedcontents) | **GET** /v1/cachedContents | List Cached Contents
*GeminiApi* | [**ListChunks**](docs/GeminiApi.md#listchunks) | **GET** /v1/corpora/{corpus}/documents/{document}/chunks | List Chunks
*GeminiApi* | [**ListCorpora**](docs/GeminiApi.md#listcorpora) | **GET** /v1/corpora | List Corpora
*GeminiApi* | [**ListDocuments**](docs/GeminiApi.md#listdocuments) | **GET** /v1/corpora/{corpus}/documents | List Documents
*GeminiApi* | [**ListFiles**](docs/GeminiApi.md#listfiles) | **GET** /v1/files | List Files
*GeminiApi* | [**ListModels**](docs/GeminiApi.md#listmodels) | **GET** /v1/models | List Models
*GeminiApi* | [**ListOperations**](docs/GeminiApi.md#listoperations) | **GET** /v1/operations | List Operations
*GeminiApi* | [**ListTunedModels**](docs/GeminiApi.md#listtunedmodels) | **GET** /v1/tunedModels | List Tuned Models
*GeminiApi* | [**QueryCorpus**](docs/GeminiApi.md#querycorpus) | **POST** /v1/corpora/{corpus}:query | Query Corpus
*GeminiApi* | [**StreamGenerateContent**](docs/GeminiApi.md#streamgeneratecontent) | **POST** /v1/models/{model}:streamGenerateContent | Stream Generate Content
*GeminiApi* | [**UpdateCachedContent**](docs/GeminiApi.md#updatecachedcontent) | **PATCH** /v1/cachedContents/{name} | Update Cached Content
*GeminiApi* | [**UpdateChunk**](docs/GeminiApi.md#updatechunk) | **PATCH** /v1/corpora/{corpus}/documents/{document}/chunks/{chunk} | Update Chunk
*GeminiApi* | [**UpdateCorpus**](docs/GeminiApi.md#updatecorpus) | **PATCH** /v1/corpora/{name} | Update Corpus
*GeminiApi* | [**UpdateDocument**](docs/GeminiApi.md#updatedocument) | **PATCH** /v1/corpora/{corpus}/documents/{document} | Update Document
*GeminiApi* | [**UpdateTunedModel**](docs/GeminiApi.md#updatetunedmodel) | **PATCH** /v1/tunedModels/{name} | Update Tuned Model
*GeminiApi* | [**UploadFile**](docs/GeminiApi.md#uploadfile) | **POST** /v1/files | Upload File
*GeminiApi* | [**UploadMedia**](docs/GeminiApi.md#uploadmedia) | **POST** /v1/media | Upload Media



## Documentation for Models

 - [Model.ApiErrorResponse](docs/ApiErrorResponse.md)
 - [Model.ApiErrorResponseError](docs/ApiErrorResponseError.md)
 - [Model.AttributionSourceId](docs/AttributionSourceId.md)
 - [Model.BatchEmbedContents200Response](docs/BatchEmbedContents200Response.md)
 - [Model.BatchEmbedContentsRequest](docs/BatchEmbedContentsRequest.md)
 - [Model.BatchEmbedContentsRequestRequestsInner](docs/BatchEmbedContentsRequestRequestsInner.md)
 - [Model.CachedContent](docs/CachedContent.md)
 - [Model.CachedContentUsageMetadata](docs/CachedContentUsageMetadata.md)
 - [Model.Candidate](docs/Candidate.md)
 - [Model.CandidateLogprobs](docs/CandidateLogprobs.md)
 - [Model.Chunk](docs/Chunk.md)
 - [Model.ChunkData](docs/ChunkData.md)
 - [Model.CitationMetadata](docs/CitationMetadata.md)
 - [Model.CitationSource](docs/CitationSource.md)
 - [Model.CodeExecutionResult](docs/CodeExecutionResult.md)
 - [Model.Condition](docs/Condition.md)
 - [Model.ContentEmbedding](docs/ContentEmbedding.md)
 - [Model.Corpus](docs/Corpus.md)
 - [Model.CountTokens200Response](docs/CountTokens200Response.md)
 - [Model.CreateTunedModelRequest](docs/CreateTunedModelRequest.md)
 - [Model.CustomMetadata](docs/CustomMetadata.md)
 - [Model.Dataset](docs/Dataset.md)
 - [Model.Document](docs/Document.md)
 - [Model.DynamicRetrievalConfig](docs/DynamicRetrievalConfig.md)
 - [Model.EmbedContentRequest](docs/EmbedContentRequest.md)
 - [Model.ExecutableCode](docs/ExecutableCode.md)
 - [Model.File](docs/File.md)
 - [Model.FileData](docs/FileData.md)
 - [Model.FunctionCall](docs/FunctionCall.md)
 - [Model.FunctionCallingConfig](docs/FunctionCallingConfig.md)
 - [Model.FunctionDeclaration](docs/FunctionDeclaration.md)
 - [Model.FunctionResponse](docs/FunctionResponse.md)
 - [Model.GeminiModel](docs/GeminiModel.md)
 - [Model.GenerateContentRequest](docs/GenerateContentRequest.md)
 - [Model.GenerateContentResponse](docs/GenerateContentResponse.md)
 - [Model.GenerateImageRequest](docs/GenerateImageRequest.md)
 - [Model.GenerateImageResponse](docs/GenerateImageResponse.md)
 - [Model.GeneratedImage](docs/GeneratedImage.md)
 - [Model.GenerationConfig](docs/GenerationConfig.md)
 - [Model.GoogleSearchRetrieval](docs/GoogleSearchRetrieval.md)
 - [Model.GroundingAttribution](docs/GroundingAttribution.md)
 - [Model.GroundingChunk](docs/GroundingChunk.md)
 - [Model.GroundingChunkWeb](docs/GroundingChunkWeb.md)
 - [Model.GroundingMetadata](docs/GroundingMetadata.md)
 - [Model.GroundingPassageId](docs/GroundingPassageId.md)
 - [Model.GroundingSupport](docs/GroundingSupport.md)
 - [Model.Hyperparameters](docs/Hyperparameters.md)
 - [Model.InlineData](docs/InlineData.md)
 - [Model.ListCachedContents200Response](docs/ListCachedContents200Response.md)
 - [Model.ListChunks200Response](docs/ListChunks200Response.md)
 - [Model.ListCorpora200Response](docs/ListCorpora200Response.md)
 - [Model.ListDocuments200Response](docs/ListDocuments200Response.md)
 - [Model.ListFiles200Response](docs/ListFiles200Response.md)
 - [Model.ListModels200Response](docs/ListModels200Response.md)
 - [Model.ListOperations200Response](docs/ListOperations200Response.md)
 - [Model.ListTunedModels200Response](docs/ListTunedModels200Response.md)
 - [Model.LogprobsResult](docs/LogprobsResult.md)
 - [Model.MetadataFilter](docs/MetadataFilter.md)
 - [Model.Operation](docs/Operation.md)
 - [Model.PromptFeedback](docs/PromptFeedback.md)
 - [Model.QueryCorpusRequest](docs/QueryCorpusRequest.md)
 - [Model.QueryCorpusResponse](docs/QueryCorpusResponse.md)
 - [Model.RelevantChunk](docs/RelevantChunk.md)
 - [Model.RequestContent](docs/RequestContent.md)
 - [Model.RequestContentPart](docs/RequestContentPart.md)
 - [Model.ResponseContent](docs/ResponseContent.md)
 - [Model.ResponseContentPart](docs/ResponseContentPart.md)
 - [Model.RetrievalMetadata](docs/RetrievalMetadata.md)
 - [Model.SafetyRating](docs/SafetyRating.md)
 - [Model.SafetySetting](docs/SafetySetting.md)
 - [Model.SearchEntryPoint](docs/SearchEntryPoint.md)
 - [Model.Segment](docs/Segment.md)
 - [Model.SemanticRetrieverChunk](docs/SemanticRetrieverChunk.md)
 - [Model.Status](docs/Status.md)
 - [Model.StringList](docs/StringList.md)
 - [Model.Tool](docs/Tool.md)
 - [Model.ToolConfig](docs/ToolConfig.md)
 - [Model.TopCandidates](docs/TopCandidates.md)
 - [Model.TunedModel](docs/TunedModel.md)
 - [Model.TunedModelSource](docs/TunedModelSource.md)
 - [Model.TuningExample](docs/TuningExample.md)
 - [Model.TuningExamples](docs/TuningExamples.md)
 - [Model.TuningSnapshot](docs/TuningSnapshot.md)
 - [Model.TuningTask](docs/TuningTask.md)
 - [Model.UploadFileRequestMetadata](docs/UploadFileRequestMetadata.md)
 - [Model.UsageMetadata](docs/UsageMetadata.md)
 - [Model.VideoMetadata](docs/VideoMetadata.md)



## Documentation for Authorization

The API key can be provided in two ways: as a header or as a query parameter. You only need to use one of these methods.

### 1. As a Header (`ApiKeyHeader`)

- **Type**: API key
- **Header Name**: `x-goog-api-key`
- **Location**: HTTP header

Example configuration:
```csharp
var config = new Configuration();
config.ApiKey.Add("x-goog-api-key", "YOUR_API_KEY");
```

### 2. As a Query Parameter (`ApiKeyQuery`)

- **Type**: API key
- **Parameter Name**: `key`
- **Location**: URL query string

Example configuration:
```csharp
var config = new Configuration();
config.ApiKey.Add("key", "YOUR_API_KEY");
```



## Contributing

We welcome contributions! To get started:

1. **Fork** the repository.
2. **Create** a new branch (`feature-branch-name`).
3. **Make** your changes and **commit** them.
4. **Push** your branch to your fork.
5. **Open** a Pull Request (PR) with a clear description of your changes.

Visit the [issues section](https://github.com/dprakash2101/GeminiSharp/issues) to discuss ideas or report issues.


## License

This project is licensed under the [MIT License](https://github.com/dprakash2101/GeminiSharp/blob/master/LICENSE).



## Author

**[Devi Prakash](https://github.com/dprakash2101)**
