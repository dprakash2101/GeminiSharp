# GeminiSharp.Api.GeminiApi

All URIs are relative to *https://generativelanguage.googleapis.com*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BatchEmbedContents**](GeminiApi.md#batchembedcontents) | **POST** /v1/models/{model}:batchEmbedContents | Batch Embed Contents |
| [**CancelOperation**](GeminiApi.md#canceloperation) | **DELETE** /v1/operations/{name} | Cancel Operation |
| [**CountTokens**](GeminiApi.md#counttokens) | **POST** /v1/models/{model}:countTokens | Count Tokens |
| [**CreateCachedContent**](GeminiApi.md#createcachedcontent) | **POST** /v1/cachedContents | Create Cached Content |
| [**CreateChunk**](GeminiApi.md#createchunk) | **POST** /v1/corpora/{corpus}/documents/{document}/chunks | Create Chunk |
| [**CreateCorpus**](GeminiApi.md#createcorpus) | **POST** /v1/corpora | Create Corpus |
| [**CreateDocument**](GeminiApi.md#createdocument) | **POST** /v1/corpora/{corpus}/documents | Create Document |
| [**CreateTunedModel**](GeminiApi.md#createtunedmodel) | **POST** /v1/tunedModels | Create Tuned Model |
| [**DeleteCachedContent**](GeminiApi.md#deletecachedcontent) | **DELETE** /v1/cachedContents/{name} | Delete Cached Content |
| [**DeleteChunk**](GeminiApi.md#deletechunk) | **DELETE** /v1/corpora/{corpus}/documents/{document}/chunks/{chunk} | Delete Chunk |
| [**DeleteCorpus**](GeminiApi.md#deletecorpus) | **DELETE** /v1/corpora/{name} | Delete Corpus |
| [**DeleteDocument**](GeminiApi.md#deletedocument) | **DELETE** /v1/corpora/{corpus}/documents/{document} | Delete Document |
| [**DeleteFile**](GeminiApi.md#deletefile) | **DELETE** /v1/files/{name} | Delete File |
| [**DeleteTunedModel**](GeminiApi.md#deletetunedmodel) | **DELETE** /v1/tunedModels/{name} | Delete Tuned Model |
| [**EmbedContent**](GeminiApi.md#embedcontent) | **POST** /v1/models/{model}:embedContent | Embed Content |
| [**GenerateContent**](GeminiApi.md#generatecontent) | **POST** /v1/models/{model}:generateContent | Generate Content |
| [**GenerateImage**](GeminiApi.md#generateimage) | **POST** /v1/models/{model}:generateImage | Generate Image |
| [**GetCachedContent**](GeminiApi.md#getcachedcontent) | **GET** /v1/cachedContents/{name} | Get Cached Content |
| [**GetChunk**](GeminiApi.md#getchunk) | **GET** /v1/corpora/{corpus}/documents/{document}/chunks/{chunk} | Get Chunk |
| [**GetCorpus**](GeminiApi.md#getcorpus) | **GET** /v1/corpora/{name} | Get Corpus |
| [**GetDocument**](GeminiApi.md#getdocument) | **GET** /v1/corpora/{corpus}/documents/{document} | Get Document |
| [**GetFile**](GeminiApi.md#getfile) | **GET** /v1/files/{name} | Get File |
| [**GetModel**](GeminiApi.md#getmodel) | **GET** /v1/models/{model} | Get Model |
| [**GetOperation**](GeminiApi.md#getoperation) | **GET** /v1/operations/{name} | Get Operation |
| [**GetTunedModel**](GeminiApi.md#gettunedmodel) | **GET** /v1/tunedModels/{name} | Get Tuned Model |
| [**ListCachedContents**](GeminiApi.md#listcachedcontents) | **GET** /v1/cachedContents | List Cached Contents |
| [**ListChunks**](GeminiApi.md#listchunks) | **GET** /v1/corpora/{corpus}/documents/{document}/chunks | List Chunks |
| [**ListCorpora**](GeminiApi.md#listcorpora) | **GET** /v1/corpora | List Corpora |
| [**ListDocuments**](GeminiApi.md#listdocuments) | **GET** /v1/corpora/{corpus}/documents | List Documents |
| [**ListFiles**](GeminiApi.md#listfiles) | **GET** /v1/files | List Files |
| [**ListModels**](GeminiApi.md#listmodels) | **GET** /v1/models | List Models |
| [**ListOperations**](GeminiApi.md#listoperations) | **GET** /v1/operations | List Operations |
| [**ListTunedModels**](GeminiApi.md#listtunedmodels) | **GET** /v1/tunedModels | List Tuned Models |
| [**QueryCorpus**](GeminiApi.md#querycorpus) | **POST** /v1/corpora/{corpus}:query | Query Corpus |
| [**StreamGenerateContent**](GeminiApi.md#streamgeneratecontent) | **POST** /v1/models/{model}:streamGenerateContent | Stream Generate Content |
| [**UpdateCachedContent**](GeminiApi.md#updatecachedcontent) | **PATCH** /v1/cachedContents/{name} | Update Cached Content |
| [**UpdateChunk**](GeminiApi.md#updatechunk) | **PATCH** /v1/corpora/{corpus}/documents/{document}/chunks/{chunk} | Update Chunk |
| [**UpdateCorpus**](GeminiApi.md#updatecorpus) | **PATCH** /v1/corpora/{name} | Update Corpus |
| [**UpdateDocument**](GeminiApi.md#updatedocument) | **PATCH** /v1/corpora/{corpus}/documents/{document} | Update Document |
| [**UpdateTunedModel**](GeminiApi.md#updatetunedmodel) | **PATCH** /v1/tunedModels/{name} | Update Tuned Model |
| [**UploadFile**](GeminiApi.md#uploadfile) | **POST** /v1/files | Upload File |
| [**UploadMedia**](GeminiApi.md#uploadmedia) | **POST** /v1/media | Upload Media |

<a id="batchembedcontents"></a>
# **BatchEmbedContents**
> BatchEmbedContents200Response BatchEmbedContents (string model, BatchEmbedContentsRequest batchEmbedContentsRequest)

Batch Embed Contents

Generates embeddings for multiple pieces of content in a single request.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class BatchEmbedContentsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

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
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.BatchEmbedContents: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatchEmbedContentsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Batch Embed Contents
    ApiResponse<BatchEmbedContents200Response> response = apiInstance.BatchEmbedContentsWithHttpInfo(model, batchEmbedContentsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.BatchEmbedContentsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  |  |
| **batchEmbedContentsRequest** | [**BatchEmbedContentsRequest**](BatchEmbedContentsRequest.md) |  |  |

### Return type

[**BatchEmbedContents200Response**](BatchEmbedContents200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Batch embedding response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="canceloperation"></a>
# **CancelOperation**
> void CancelOperation (string name)

Cancel Operation

Cancels a long-running operation.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CancelOperationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Cancel Operation
                apiInstance.CancelOperation(name);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CancelOperation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CancelOperationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Cancel Operation
    apiInstance.CancelOperationWithHttpInfo(name);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CancelOperationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Operation cancelled |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="counttokens"></a>
# **CountTokens**
> CountTokens200Response CountTokens (string model, GenerateContentRequest generateContentRequest)

Count Tokens

Counts the number of tokens in the given prompt.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CountTokensExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = "model_example";  // string | 
            var generateContentRequest = new GenerateContentRequest(); // GenerateContentRequest | 

            try
            {
                // Count Tokens
                CountTokens200Response result = apiInstance.CountTokens(model, generateContentRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CountTokens: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CountTokensWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Count Tokens
    ApiResponse<CountTokens200Response> response = apiInstance.CountTokensWithHttpInfo(model, generateContentRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CountTokensWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  |  |
| **generateContentRequest** | [**GenerateContentRequest**](GenerateContentRequest.md) |  |  |

### Return type

[**CountTokens200Response**](CountTokens200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Token count response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createcachedcontent"></a>
# **CreateCachedContent**
> CachedContent CreateCachedContent (CachedContent cachedContent)

Create Cached Content

Creates cached content for efficient context reuse in subsequent requests.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CreateCachedContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var cachedContent = new CachedContent(); // CachedContent | 

            try
            {
                // Create Cached Content
                CachedContent result = apiInstance.CreateCachedContent(cachedContent);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CreateCachedContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateCachedContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Cached Content
    ApiResponse<CachedContent> response = apiInstance.CreateCachedContentWithHttpInfo(cachedContent);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CreateCachedContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **cachedContent** | [**CachedContent**](CachedContent.md) |  |  |

### Return type

[**CachedContent**](CachedContent.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Created cached content |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createchunk"></a>
# **CreateChunk**
> Chunk CreateChunk (string corpus, string document, Chunk chunk)

Create Chunk

Creates a chunk in a document.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CreateChunkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 
            var chunk = new Chunk(); // Chunk | 

            try
            {
                // Create Chunk
                Chunk result = apiInstance.CreateChunk(corpus, document, chunk);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CreateChunk: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateChunkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Chunk
    ApiResponse<Chunk> response = apiInstance.CreateChunkWithHttpInfo(corpus, document, chunk);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CreateChunkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |
| **chunk** | [**Chunk**](Chunk.md) |  |  |

### Return type

[**Chunk**](Chunk.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Created chunk |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createcorpus"></a>
# **CreateCorpus**
> Corpus CreateCorpus (Corpus corpus)

Create Corpus

Creates a corpus for semantic retrieval.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CreateCorpusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = new Corpus(); // Corpus | 

            try
            {
                // Create Corpus
                Corpus result = apiInstance.CreateCorpus(corpus);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CreateCorpus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateCorpusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Corpus
    ApiResponse<Corpus> response = apiInstance.CreateCorpusWithHttpInfo(corpus);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CreateCorpusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | [**Corpus**](Corpus.md) |  |  |

### Return type

[**Corpus**](Corpus.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Created corpus |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createdocument"></a>
# **CreateDocument**
> Document CreateDocument (string corpus, Document document)

Create Document

Creates a document in a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CreateDocumentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = new Document(); // Document | 

            try
            {
                // Create Document
                Document result = apiInstance.CreateDocument(corpus, document);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CreateDocument: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDocumentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Document
    ApiResponse<Document> response = apiInstance.CreateDocumentWithHttpInfo(corpus, document);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CreateDocumentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | [**Document**](Document.md) |  |  |

### Return type

[**Document**](Document.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Created document |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createtunedmodel"></a>
# **CreateTunedModel**
> Operation CreateTunedModel (CreateTunedModelRequest createTunedModelRequest)

Create Tuned Model

Creates a tuned model.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class CreateTunedModelExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var createTunedModelRequest = new CreateTunedModelRequest(); // CreateTunedModelRequest | 

            try
            {
                // Create Tuned Model
                Operation result = apiInstance.CreateTunedModel(createTunedModelRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.CreateTunedModel: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateTunedModelWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Tuned Model
    ApiResponse<Operation> response = apiInstance.CreateTunedModelWithHttpInfo(createTunedModelRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.CreateTunedModelWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createTunedModelRequest** | [**CreateTunedModelRequest**](CreateTunedModelRequest.md) |  |  |

### Return type

[**Operation**](Operation.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Long-running operation for model tuning |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletecachedcontent"></a>
# **DeleteCachedContent**
> void DeleteCachedContent (string name)

Delete Cached Content

Deletes cached content.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class DeleteCachedContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Delete Cached Content
                apiInstance.DeleteCachedContent(name);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.DeleteCachedContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteCachedContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Cached Content
    apiInstance.DeleteCachedContentWithHttpInfo(name);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.DeleteCachedContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Cached content deleted successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletechunk"></a>
# **DeleteChunk**
> void DeleteChunk (string corpus, string document, string chunk)

Delete Chunk

Deletes a chunk from a document.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class DeleteChunkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 
            var chunk = "chunk_example";  // string | 

            try
            {
                // Delete Chunk
                apiInstance.DeleteChunk(corpus, document, chunk);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.DeleteChunk: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteChunkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Chunk
    apiInstance.DeleteChunkWithHttpInfo(corpus, document, chunk);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.DeleteChunkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |
| **chunk** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Chunk deleted successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletecorpus"></a>
# **DeleteCorpus**
> void DeleteCorpus (string name)

Delete Corpus

Deletes a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class DeleteCorpusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Delete Corpus
                apiInstance.DeleteCorpus(name);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.DeleteCorpus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteCorpusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Corpus
    apiInstance.DeleteCorpusWithHttpInfo(name);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.DeleteCorpusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Corpus deleted successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletedocument"></a>
# **DeleteDocument**
> void DeleteDocument (string corpus, string document)

Delete Document

Deletes a document from a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class DeleteDocumentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 

            try
            {
                // Delete Document
                apiInstance.DeleteDocument(corpus, document);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.DeleteDocument: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDocumentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Document
    apiInstance.DeleteDocumentWithHttpInfo(corpus, document);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.DeleteDocumentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Document deleted successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletefile"></a>
# **DeleteFile**
> void DeleteFile (string name)

Delete File

Deletes an uploaded file.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class DeleteFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Delete File
                apiInstance.DeleteFile(name);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.DeleteFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete File
    apiInstance.DeleteFileWithHttpInfo(name);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.DeleteFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | File deleted successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletetunedmodel"></a>
# **DeleteTunedModel**
> void DeleteTunedModel (string name)

Delete Tuned Model

Deletes a tuned model.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class DeleteTunedModelExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Delete Tuned Model
                apiInstance.DeleteTunedModel(name);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.DeleteTunedModel: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteTunedModelWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Tuned Model
    apiInstance.DeleteTunedModelWithHttpInfo(name);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.DeleteTunedModelWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Tuned model deleted successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="embedcontent"></a>
# **EmbedContent**
> ContentEmbedding EmbedContent (string model, EmbedContentRequest embedContentRequest)

Embed Content

Generates an embedding representation of the given content.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class EmbedContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = models/text-embedding-004;  // string | 
            var embedContentRequest = new EmbedContentRequest(); // EmbedContentRequest | 

            try
            {
                // Embed Content
                ContentEmbedding result = apiInstance.EmbedContent(model, embedContentRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.EmbedContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmbedContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Embed Content
    ApiResponse<ContentEmbedding> response = apiInstance.EmbedContentWithHttpInfo(model, embedContentRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.EmbedContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  |  |
| **embedContentRequest** | [**EmbedContentRequest**](EmbedContentRequest.md) |  |  |

### Return type

[**ContentEmbedding**](ContentEmbedding.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Embedding response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="generatecontent"></a>
# **GenerateContent**
> GenerateContentResponse GenerateContent (string model, GenerateContentRequest generateContentRequest)

Generate Content

Generates content from the model given an input GenerateContentRequest.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GenerateContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = "\"gemini-2.0-flash\"";  // string |  (default to "gemini-2.0-flash")
            var generateContentRequest = new GenerateContentRequest(); // GenerateContentRequest | 

            try
            {
                // Generate Content
                GenerateContentResponse result = apiInstance.GenerateContent(model, generateContentRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GenerateContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GenerateContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Generate Content
    ApiResponse<GenerateContentResponse> response = apiInstance.GenerateContentWithHttpInfo(model, generateContentRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GenerateContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  | [default to &quot;gemini-2.0-flash&quot;] |
| **generateContentRequest** | [**GenerateContentRequest**](GenerateContentRequest.md) |  |  |

### Return type

[**GenerateContentResponse**](GenerateContentResponse.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful response |  -  |
| **0** | Error response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="generateimage"></a>
# **GenerateImage**
> GenerateImageResponse GenerateImage (string model, GenerateImageRequest generateImageRequest)

Generate Image

Generates images based on text prompts using Gemini image generation models.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GenerateImageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = "models/gemini-1.5-flash-image";  // string | 
            var generateImageRequest = new GenerateImageRequest(); // GenerateImageRequest | 

            try
            {
                // Generate Image
                GenerateImageResponse result = apiInstance.GenerateImage(model, generateImageRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GenerateImage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GenerateImageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Generate Image
    ApiResponse<GenerateImageResponse> response = apiInstance.GenerateImageWithHttpInfo(model, generateImageRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GenerateImageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  |  |
| **generateImageRequest** | [**GenerateImageRequest**](GenerateImageRequest.md) |  |  |

### Return type

[**GenerateImageResponse**](GenerateImageResponse.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Generated image response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getcachedcontent"></a>
# **GetCachedContent**
> CachedContent GetCachedContent (string name)

Get Cached Content

Gets a cached content by name.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetCachedContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Get Cached Content
                CachedContent result = apiInstance.GetCachedContent(name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetCachedContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetCachedContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Cached Content
    ApiResponse<CachedContent> response = apiInstance.GetCachedContentWithHttpInfo(name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetCachedContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

[**CachedContent**](CachedContent.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Cached content details |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getchunk"></a>
# **GetChunk**
> Chunk GetChunk (string corpus, string document, string chunk)

Get Chunk

Gets a chunk from a document.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetChunkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 
            var chunk = "chunk_example";  // string | 

            try
            {
                // Get Chunk
                Chunk result = apiInstance.GetChunk(corpus, document, chunk);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetChunk: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetChunkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Chunk
    ApiResponse<Chunk> response = apiInstance.GetChunkWithHttpInfo(corpus, document, chunk);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetChunkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |
| **chunk** | **string** |  |  |

### Return type

[**Chunk**](Chunk.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Chunk details |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getcorpus"></a>
# **GetCorpus**
> Corpus GetCorpus (string name)

Get Corpus

Gets a corpus by name.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetCorpusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Get Corpus
                Corpus result = apiInstance.GetCorpus(name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetCorpus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetCorpusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Corpus
    ApiResponse<Corpus> response = apiInstance.GetCorpusWithHttpInfo(name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetCorpusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

[**Corpus**](Corpus.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Corpus details |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdocument"></a>
# **GetDocument**
> Document GetDocument (string corpus, string document)

Get Document

Gets a document from a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetDocumentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 

            try
            {
                // Get Document
                Document result = apiInstance.GetDocument(corpus, document);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetDocument: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDocumentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Document
    ApiResponse<Document> response = apiInstance.GetDocumentWithHttpInfo(corpus, document);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetDocumentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |

### Return type

[**Document**](Document.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Document details |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getfile"></a>
# **GetFile**
> File GetFile (string name)

Get File

Gets the metadata for a specific uploaded file.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = files/sample-file-id;  // string | 

            try
            {
                // Get File
                File result = apiInstance.GetFile(name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get File
    ApiResponse<File> response = apiInstance.GetFileWithHttpInfo(name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

[**File**](File.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | File information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getmodel"></a>
# **GetModel**
> GeminiModel GetModel (string model)

Get Model

Gets detailed information about a specific model.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetModelExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = "models/gemini-1.5-flash";  // string | 

            try
            {
                // Get Model
                GeminiModel result = apiInstance.GetModel(model);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetModel: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetModelWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Model
    ApiResponse<GeminiModel> response = apiInstance.GetModelWithHttpInfo(model);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetModelWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  |  |

### Return type

[**GeminiModel**](GeminiModel.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Model information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getoperation"></a>
# **GetOperation**
> Operation GetOperation (string name)

Get Operation

Gets the status of a long-running operation.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetOperationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Get Operation
                Operation result = apiInstance.GetOperation(name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetOperation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetOperationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Operation
    ApiResponse<Operation> response = apiInstance.GetOperationWithHttpInfo(name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetOperationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

[**Operation**](Operation.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Operation status |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="gettunedmodel"></a>
# **GetTunedModel**
> TunedModel GetTunedModel (string name)

Get Tuned Model

Gets information about a specific tuned model.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class GetTunedModelExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 

            try
            {
                // Get Tuned Model
                TunedModel result = apiInstance.GetTunedModel(name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.GetTunedModel: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetTunedModelWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Tuned Model
    ApiResponse<TunedModel> response = apiInstance.GetTunedModelWithHttpInfo(name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.GetTunedModelWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |

### Return type

[**TunedModel**](TunedModel.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Tuned model information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listcachedcontents"></a>
# **ListCachedContents**
> ListCachedContents200Response ListCachedContents (int? pageSize = null, string? pageToken = null)

List Cached Contents

Lists cached contents for efficient context reuse.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListCachedContentsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 

            try
            {
                // List Cached Contents
                ListCachedContents200Response result = apiInstance.ListCachedContents(pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListCachedContents: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListCachedContentsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Cached Contents
    ApiResponse<ListCachedContents200Response> response = apiInstance.ListCachedContentsWithHttpInfo(pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListCachedContentsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |

### Return type

[**ListCachedContents200Response**](ListCachedContents200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of cached contents |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listchunks"></a>
# **ListChunks**
> ListChunks200Response ListChunks (string corpus, string document, int? pageSize = null, string? pageToken = null)

List Chunks

Lists chunks in a document.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListChunksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 

            try
            {
                // List Chunks
                ListChunks200Response result = apiInstance.ListChunks(corpus, document, pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListChunks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListChunksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Chunks
    ApiResponse<ListChunks200Response> response = apiInstance.ListChunksWithHttpInfo(corpus, document, pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListChunksWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |

### Return type

[**ListChunks200Response**](ListChunks200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of chunks |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listcorpora"></a>
# **ListCorpora**
> ListCorpora200Response ListCorpora (int? pageSize = null, string? pageToken = null)

List Corpora

Lists corpora for semantic retrieval.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListCorporaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 

            try
            {
                // List Corpora
                ListCorpora200Response result = apiInstance.ListCorpora(pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListCorpora: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListCorporaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Corpora
    ApiResponse<ListCorpora200Response> response = apiInstance.ListCorporaWithHttpInfo(pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListCorporaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |

### Return type

[**ListCorpora200Response**](ListCorpora200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of corpora |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listdocuments"></a>
# **ListDocuments**
> ListDocuments200Response ListDocuments (string corpus, int? pageSize = null, string? pageToken = null)

List Documents

Lists documents in a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListDocumentsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 

            try
            {
                // List Documents
                ListDocuments200Response result = apiInstance.ListDocuments(corpus, pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListDocuments: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDocumentsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Documents
    ApiResponse<ListDocuments200Response> response = apiInstance.ListDocumentsWithHttpInfo(corpus, pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListDocumentsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |

### Return type

[**ListDocuments200Response**](ListDocuments200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of documents |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listfiles"></a>
# **ListFiles**
> ListFiles200Response ListFiles (int? pageSize = null, string? pageToken = null)

List Files

Lists uploaded files.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListFilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 

            try
            {
                // List Files
                ListFiles200Response result = apiInstance.ListFiles(pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListFiles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListFilesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Files
    ApiResponse<ListFiles200Response> response = apiInstance.ListFilesWithHttpInfo(pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListFilesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |

### Return type

[**ListFiles200Response**](ListFiles200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of files |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listmodels"></a>
# **ListModels**
> ListModels200Response ListModels (int? pageSize = null, string? pageToken = null)

List Models

Lists all available Gemini models including base models and tuned models.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListModelsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? | Maximum number of models to return (optional) 
            var pageToken = "pageToken_example";  // string? | Token for pagination (optional) 

            try
            {
                // List Models
                ListModels200Response result = apiInstance.ListModels(pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListModels: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListModelsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Models
    ApiResponse<ListModels200Response> response = apiInstance.ListModelsWithHttpInfo(pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListModelsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | Maximum number of models to return | [optional]  |
| **pageToken** | **string?** | Token for pagination | [optional]  |

### Return type

[**ListModels200Response**](ListModels200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A list of models |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listoperations"></a>
# **ListOperations**
> ListOperations200Response ListOperations (string? name = null, string? filter = null, int? pageSize = null, string? pageToken = null)

List Operations

Lists operations that match the specified filter.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListOperationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string? | The name of the operation's parent resource (optional) 
            var filter = "filter_example";  // string? |  (optional) 
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 

            try
            {
                // List Operations
                ListOperations200Response result = apiInstance.ListOperations(name, filter, pageSize, pageToken);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListOperations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListOperationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Operations
    ApiResponse<ListOperations200Response> response = apiInstance.ListOperationsWithHttpInfo(name, filter, pageSize, pageToken);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListOperationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string?** | The name of the operation&#39;s parent resource | [optional]  |
| **filter** | **string?** |  | [optional]  |
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |

### Return type

[**ListOperations200Response**](ListOperations200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of operations |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listtunedmodels"></a>
# **ListTunedModels**
> ListTunedModels200Response ListTunedModels (int? pageSize = null, string? pageToken = null, string? filter = null)

List Tuned Models

Lists tuned models owned by the user.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class ListTunedModelsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var pageSize = 56;  // int? |  (optional) 
            var pageToken = "pageToken_example";  // string? |  (optional) 
            var filter = "filter_example";  // string? | Filter expression to list tuned models (optional) 

            try
            {
                // List Tuned Models
                ListTunedModels200Response result = apiInstance.ListTunedModels(pageSize, pageToken, filter);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.ListTunedModels: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListTunedModelsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Tuned Models
    ApiResponse<ListTunedModels200Response> response = apiInstance.ListTunedModelsWithHttpInfo(pageSize, pageToken, filter);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.ListTunedModelsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** |  | [optional]  |
| **pageToken** | **string?** |  | [optional]  |
| **filter** | **string?** | Filter expression to list tuned models | [optional]  |

### Return type

[**ListTunedModels200Response**](ListTunedModels200Response.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | List of tuned models |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="querycorpus"></a>
# **QueryCorpus**
> QueryCorpusResponse QueryCorpus (string corpus, QueryCorpusRequest queryCorpusRequest)

Query Corpus

Performs a semantic search query against a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class QueryCorpusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var queryCorpusRequest = new QueryCorpusRequest(); // QueryCorpusRequest | 

            try
            {
                // Query Corpus
                QueryCorpusResponse result = apiInstance.QueryCorpus(corpus, queryCorpusRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.QueryCorpus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the QueryCorpusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Query Corpus
    ApiResponse<QueryCorpusResponse> response = apiInstance.QueryCorpusWithHttpInfo(corpus, queryCorpusRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.QueryCorpusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **queryCorpusRequest** | [**QueryCorpusRequest**](QueryCorpusRequest.md) |  |  |

### Return type

[**QueryCorpusResponse**](QueryCorpusResponse.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Query results |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="streamgeneratecontent"></a>
# **StreamGenerateContent**
> string StreamGenerateContent (string model, GenerateContentRequest generateContentRequest)

Stream Generate Content

Generates streaming content from the model.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class StreamGenerateContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var model = "models/gemini-1.5-flash";  // string | 
            var generateContentRequest = new GenerateContentRequest(); // GenerateContentRequest | 

            try
            {
                // Stream Generate Content
                string result = apiInstance.StreamGenerateContent(model, generateContentRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.StreamGenerateContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StreamGenerateContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Stream Generate Content
    ApiResponse<string> response = apiInstance.StreamGenerateContentWithHttpInfo(model, generateContentRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.StreamGenerateContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **model** | **string** |  |  |
| **generateContentRequest** | [**GenerateContentRequest**](GenerateContentRequest.md) |  |  |

### Return type

**string**

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Streaming response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatecachedcontent"></a>
# **UpdateCachedContent**
> CachedContent UpdateCachedContent (string name, CachedContent cachedContent, string? updateMask = null)

Update Cached Content

Updates a cached content.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UpdateCachedContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 
            var cachedContent = new CachedContent(); // CachedContent | 
            var updateMask = "updateMask_example";  // string? |  (optional) 

            try
            {
                // Update Cached Content
                CachedContent result = apiInstance.UpdateCachedContent(name, cachedContent, updateMask);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UpdateCachedContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateCachedContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Cached Content
    ApiResponse<CachedContent> response = apiInstance.UpdateCachedContentWithHttpInfo(name, cachedContent, updateMask);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UpdateCachedContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |
| **cachedContent** | [**CachedContent**](CachedContent.md) |  |  |
| **updateMask** | **string?** |  | [optional]  |

### Return type

[**CachedContent**](CachedContent.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated cached content |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatechunk"></a>
# **UpdateChunk**
> Chunk UpdateChunk (string corpus, string document, string chunk, Chunk chunk2, string? updateMask = null)

Update Chunk

Updates a chunk in a document.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UpdateChunkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 
            var chunk = "chunk_example";  // string | 
            var chunk2 = new Chunk(); // Chunk | 
            var updateMask = "updateMask_example";  // string? |  (optional) 

            try
            {
                // Update Chunk
                Chunk result = apiInstance.UpdateChunk(corpus, document, chunk, chunk2, updateMask);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UpdateChunk: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateChunkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Chunk
    ApiResponse<Chunk> response = apiInstance.UpdateChunkWithHttpInfo(corpus, document, chunk, chunk2, updateMask);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UpdateChunkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |
| **chunk** | **string** |  |  |
| **chunk2** | [**Chunk**](Chunk.md) |  |  |
| **updateMask** | **string?** |  | [optional]  |

### Return type

[**Chunk**](Chunk.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated chunk |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatecorpus"></a>
# **UpdateCorpus**
> Corpus UpdateCorpus (string name, Corpus corpus, string? updateMask = null)

Update Corpus

Updates a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UpdateCorpusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 
            var corpus = new Corpus(); // Corpus | 
            var updateMask = "updateMask_example";  // string? |  (optional) 

            try
            {
                // Update Corpus
                Corpus result = apiInstance.UpdateCorpus(name, corpus, updateMask);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UpdateCorpus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateCorpusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Corpus
    ApiResponse<Corpus> response = apiInstance.UpdateCorpusWithHttpInfo(name, corpus, updateMask);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UpdateCorpusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |
| **corpus** | [**Corpus**](Corpus.md) |  |  |
| **updateMask** | **string?** |  | [optional]  |

### Return type

[**Corpus**](Corpus.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated corpus |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatedocument"></a>
# **UpdateDocument**
> Document UpdateDocument (string corpus, string document, Document document2, string? updateMask = null)

Update Document

Updates a document in a corpus.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UpdateDocumentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var corpus = "corpus_example";  // string | 
            var document = "document_example";  // string | 
            var document2 = new Document(); // Document | 
            var updateMask = "updateMask_example";  // string? |  (optional) 

            try
            {
                // Update Document
                Document result = apiInstance.UpdateDocument(corpus, document, document2, updateMask);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UpdateDocument: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDocumentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Document
    ApiResponse<Document> response = apiInstance.UpdateDocumentWithHttpInfo(corpus, document, document2, updateMask);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UpdateDocumentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **corpus** | **string** |  |  |
| **document** | **string** |  |  |
| **document2** | [**Document**](Document.md) |  |  |
| **updateMask** | **string?** |  | [optional]  |

### Return type

[**Document**](Document.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated document |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatetunedmodel"></a>
# **UpdateTunedModel**
> TunedModel UpdateTunedModel (string name, TunedModel tunedModel, string? updateMask = null)

Update Tuned Model

Updates a tuned model.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UpdateTunedModelExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var name = "name_example";  // string | 
            var tunedModel = new TunedModel(); // TunedModel | 
            var updateMask = "updateMask_example";  // string? | Field mask to specify which fields to update (optional) 

            try
            {
                // Update Tuned Model
                TunedModel result = apiInstance.UpdateTunedModel(name, tunedModel, updateMask);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UpdateTunedModel: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateTunedModelWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Tuned Model
    ApiResponse<TunedModel> response = apiInstance.UpdateTunedModelWithHttpInfo(name, tunedModel, updateMask);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UpdateTunedModelWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **name** | **string** |  |  |
| **tunedModel** | [**TunedModel**](TunedModel.md) |  |  |
| **updateMask** | **string?** | Field mask to specify which fields to update | [optional]  |

### Return type

[**TunedModel**](TunedModel.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated tuned model |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uploadfile"></a>
# **UploadFile**
> File UploadFile (FileParameter? file = null, UploadFileRequestMetadata? metadata = null)

Upload File

Creates a File by uploading to the API.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UploadFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var file = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // FileParameter? |  (optional) 
            var metadata = new UploadFileRequestMetadata?(); // UploadFileRequestMetadata? |  (optional) 

            try
            {
                // Upload File
                File result = apiInstance.UploadFile(file, metadata);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UploadFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UploadFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload File
    ApiResponse<File> response = apiInstance.UploadFileWithHttpInfo(file, metadata);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UploadFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **file** | **FileParameter?****FileParameter?** |  | [optional]  |
| **metadata** | [**UploadFileRequestMetadata?**](UploadFileRequestMetadata?.md) |  | [optional]  |

### Return type

[**File**](File.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Uploaded file information |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="uploadmedia"></a>
# **UploadMedia**
> File UploadMedia (FileParameter? file = null)

Upload Media

Uploads media files for processing with Gemini models.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using GeminiSharp.Api;
using GeminiSharp.Client;
using GeminiSharp.Model;

namespace Example
{
    public class UploadMediaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://generativelanguage.googleapis.com";
            // Configure API key authorization: ApiKeyHeader
            config.AddApiKey("x-goog-api-key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("x-goog-api-key", "Bearer");
            // Configure API key authorization: ApiKeyQuery
            config.AddApiKey("key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("key", "Bearer");

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new GeminiApi(httpClient, config, httpClientHandler);
            var file = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // FileParameter? |  (optional) 

            try
            {
                // Upload Media
                File result = apiInstance.UploadMedia(file);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GeminiApi.UploadMedia: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UploadMediaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload Media
    ApiResponse<File> response = apiInstance.UploadMediaWithHttpInfo(file);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GeminiApi.UploadMediaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **file** | **FileParameter?****FileParameter?** |  | [optional]  |

### Return type

[**File**](File.md)

### Authorization

[ApiKeyHeader](../README.md#ApiKeyHeader), [ApiKeyQuery](../README.md#ApiKeyQuery)

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Media uploaded successfully |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

