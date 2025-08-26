# Implementing Retry Logic

The GeminiSharp client is integrated with the [Polly](https://github.com/App-vNext/Polly) library to provide robust and configurable retry mechanisms for API requests. This allows you to make your application more resilient to transient network issues or temporary server-side errors.

## How It Works

The `GeminiSharp.Client.ApiClient` will automatically use any Polly retry policy that you assign to the static `RetryConfiguration.AsyncRetryPolicy` property. If no policy is assigned, requests will not be retried.

## How to Enable Retries

To enable retries, you need to create a Polly `IAsyncPolicy<HttpResponseMessage>` and assign it to `RetryConfiguration.AsyncRetryPolicy`.

### Prerequisites

You will need to add the `Polly.Extensions.Http` NuGet package to your project to get access to helpful extension methods for creating HTTP retry policies.

```bash
dotnet add package Polly.Extensions.Http
```

### Example: Exponential Backoff

Here is an example of how to configure a retry policy that will:
1.  Retry up to 3 times.
2.  Use an exponential backoff strategy (waiting 2, 4, and 8 seconds between retries).
3.  Retry on transient HTTP errors (5xx status codes or `HttpRequestException`).
4.  Retry on `429 Too Many Requests` responses.

You should set this policy at the startup of your application, before you make any API calls.

```csharp
using GeminiSharp.Client;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

public class Program
{
    public static void Main(string[] args)
    {
        // Define your retry policy
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError() // Handles HttpRequestException, 5xx, and 408 responses
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // Also retry on 429
            .WaitAndRetryAsync(3, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    // Optional: log the retry attempt
                    Console.WriteLine($"Retry {retryAttempt}: Delaying for {timespan.TotalSeconds} seconds, then retrying the request.");
                }
            );

        // Assign the policy to the static configuration before creating your client
        RetryConfiguration.AsyncRetryPolicy = retryPolicy;

        // Now, create your API client and make calls as usual.
        // Any asynchronous requests will automatically use the retry policy.
        // var api = new DefaultApi();
        // var response = await api.GenerateContentAsync(...);
    }
}
```

By setting the `AsyncRetryPolicy`, all subsequent asynchronous calls made by the `ApiClient` will be wrapped in this policy, automatically handling retries as configured.
