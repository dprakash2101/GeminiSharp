# Retry Configuration for GeminiSharp SDK

In the GeminiSharp SDK, the **retry configuration** mechanism enhances your application's resilience by automatically handling transient API errors. These are temporary issues, such as network glitches or server overloads, that often resolve themselves upon retrying the request. By configuring retries, you can significantly improve the reliability of your Gemini API integrations.

---

## The `RetryConfig` Class

To customize the retry behavior, you use the `RetryConfig` class. This class provides several properties to fine-tune how the SDK retries failed API requests.

### Key Properties of `RetryConfig`:

*   **`MaxRetries`** (int):
    *   Defines the maximum number of times the SDK will re-attempt an API request after an initial failure.
    *   **Default**: `3` retries.
    *   *Example*: Setting `MaxRetries = 5` means the SDK will try the request up to 5 additional times.

*   **`InitialDelayMs`** (int):
    *   The initial waiting period (in milliseconds) before the first retry attempt.
    *   **Default**: `1000ms` (1 second).
    *   *Example*: If `InitialDelayMs = 2000`, the SDK waits 2 seconds before the first retry.

*   **`UseExponentialBackoff`** (bool):
    *   Determines whether the delay between retries should increase exponentially (e.g., doubling with each attempt) or remain constant.
    *   **Default**: `true` (exponential backoff is enabled).
    *   *Example*: If `UseExponentialBackoff = true` and `InitialDelayMs = 1000`, subsequent delays might be 1s, 2s, 4s, etc.

*   **`RetryStatusCodes`** (HashSet<int>):
    *   A collection of HTTP status codes that should trigger a retry. Common retryable codes include `429` (Too Many Requests), `500` (Internal Server Error), and `503` (Service Unavailable).
    *   **Defaults**: `{ 429, 500, 503 }`.
    *   *Example*: Adding `408` (Request Timeout) to this set will cause the SDK to retry requests returning that status.

---

## Default Retry Configuration

If you do not explicitly provide a `RetryConfig` instance, the SDK will use the following default settings:

```csharp
new RetryConfig
{
    MaxRetries = 3,                          // 3 retry attempts
    InitialDelayMs = 1000,                    // 1-second initial delay
    UseExponentialBackoff = true,             // Enable exponential backoff
    RetryStatusCodes = new HashSet<int> { 429, 500, 503 }  // Retry on 429, 500, and 503
}
```

---

## Customizing Retry Behavior

You can customize the retry behavior by configuring the `RetryConfig` when you register the GeminiSharp services in your application's dependency injection container.

```csharp
using GeminiSharp.Extensions;
using GeminiSharp.Models.Utilities;
using Microsoft.Extensions.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
    services.AddGeminiSharp(options =>
    {
        options.ApiKey = "YOUR_GEMINI_API_KEY";
        options.RetryConfig = new RetryConfig
        {
            MaxRetries = 5,                          // Retry up to 5 times
            InitialDelayMs = 2000,                   // 2-second initial delay
            UseExponentialBackoff = false,           // Use fixed delay
            RetryStatusCodes = new HashSet<int> { 429, 500, 503, 408 } // Add 408 to retryable codes
        };
    });
}
```

---

## How Retry Logic Works

The retry mechanism is integrated into the `HttpClient` pipeline using Polly. When you make an API call, the following happens:

1.  **Initial Request**: The SDK sends the API request.
2.  **Error Detection**: If a retryable HTTP status code is received, the Polly policy engages.
3.  **Delay Calculation**: If `UseExponentialBackoff` is `true`, the delay increases with each subsequent retry attempt. Otherwise, it remains constant.
4.  **Retry Attempt**: The request is re-sent.
5.  **Max Retries**: This process repeats until a successful response is received or the `MaxRetries` limit is reached. If the request still fails after all retries, a `GeminiApiException` (or a general `Exception`) is thrown.

---

## Example with an API Call

Once you've configured the retry logic, it will be automatically applied to all API calls made through the `IGeminiClient`.

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

public class RetryExample
{
    public static async Task Run(string apiKey)
    {
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(options =>
            {
                options.ApiKey = apiKey;
                options.RetryConfig = new RetryConfig { MaxRetries = 5 };
            })
            .BuildServiceProvider();

        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        try
        {
            Console.WriteLine("Attempting to generate content with retry logic...");
            var response = await geminiClient.GenerateTextAsync("gemini-1.5-flash", "Explain the concept of quantum entanglement.");

            Console.WriteLine("Successfully generated content:");
            Console.WriteLine(response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text);
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error after retries: {ex.Message}");
            Console.WriteLine($"Status Code: {ex.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred after retries: {ex.Message}");
        }
    }
}
```

---

## Logging Retry Attempts

The SDK integrates with Serilog to provide detailed logs of retry attempts. This is invaluable for monitoring and debugging your application's resilience.

Example log output (assuming Serilog is configured):

```text
[WRN] API call failed with status code 503 Service Unavailable. Retrying in 1000ms (Attempt 1/3).
[WRN] API call failed with status code 503 Service Unavailable. Retrying in 2000ms (Attempt 2/3).
[ERR] API call failed after 3 retries. Last error: Service Unavailable.
```

These logs indicate:
*   The HTTP status code that triggered the retry.
*   The current retry attempt number and the total `MaxRetries`.
*   The delay before the next retry, demonstrating exponential backoff if enabled.

---

## Conclusion

The `RetryConfig` in the GeminiSharp SDK is a robust feature designed to make your applications more resilient to transient network and API issues. By understanding and customizing its parameters, you can ensure your integrations with the Gemini API are both reliable and performant.
