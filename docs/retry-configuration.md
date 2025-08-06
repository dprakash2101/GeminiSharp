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

You can create a custom `RetryConfig` object and pass it to the `GeminiClient` constructor during initialization. The `GeminiClient` will then use this configuration for all its internal API calls.

For instance, to configure 5 retries with a 2-second fixed initial delay and include `408` as a retryable status code:

```csharp
using GeminiSharp.Client;
using GeminiSharp.Models.Utilities;
using System.Net.Http;
using System.Collections.Generic;

// 1. Define your custom RetryConfig
var customRetryConfig = new RetryConfig
{
    MaxRetries = 5,                          // Retry up to 5 times
    InitialDelayMs = 2000,                   // 2-second initial delay
    UseExponentialBackoff = false,           // Use fixed delay
    RetryStatusCodes = new HashSet<int> { 429, 500, 503, 408 } // Add 408 to retryable codes
};

// 2. Initialize HttpClient (required for GeminiClient)
using var httpClient = new HttpClient();

// 3. Initialize GeminiClient with your custom RetryConfig
// The API key and base URL are also passed here.
var apiKey = "YOUR_GEMINI_API_KEY";
var geminiClient = new GeminiClient(apiKey, httpClient, null, customRetryConfig);

// Now, any API calls made through geminiClient will use customRetryConfig
// Example: await geminiClient.GenerateTextAsync(null, "Hello");
```

---

## How Retry Logic Works

The retry mechanism is automatically engaged when the Gemini API returns a transient error (e.g., `500` or `503`). Here's the typical flow:

1.  **Initial Request**: The SDK sends the API request.
2.  **Error Detection**: If a retryable HTTP status code is received, the SDK pauses for the configured delay.
3.  **Delay Calculation**: If `UseExponentialBackoff` is `true`, the delay increases with each subsequent retry attempt. Otherwise, it remains constant.
4.  **Retry Attempt**: The request is re-sent.
5.  **Max Retries**: This process repeats until a successful response is received or the `MaxRetries` limit is reached. If the request still fails after all retries, a `GeminiApiException` (or a general `Exception`) is thrown.

---

## Example with an API Call

When you invoke any method on the `GeminiClient` (or its specialized clients), the configured retry logic transparently handles transient failures.

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class RetryExample
{
    public static async Task Run(string apiKey)
    {
        // Using default retry configuration
        using var httpClient = new HttpClient();
        var geminiClient = new GeminiClient(apiKey, httpClient);

        try
        {
            Console.WriteLine("Attempting to generate content with retry logic...");
            // This call will automatically retry if transient errors occur
            var response = await geminiClient.GenerateTextAsync(null, "Explain the concept of quantum entanglement.");

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

// To run this example:
// RetryExample.Run("YOUR_GEMINI_API_KEY").Wait();
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

For more details on specific client methods, refer to the documentation for `GeminiClient` and its specialized clients (`TextClient`, `ImageClient`, etc.).