# Retry Configuration for GeminiSharp SDK

## What is Retry Configuration?

In the GeminiSharp SDK, the **retry configuration** determines how the SDK handles transient errors when making API requests. Transient errors are temporary problems, such as network interruptions or API server overload, that may resolve if you retry the request after some time. 

The retry mechanism helps improve the reliability of your application by automatically retrying failed requests, which reduces the chances of your app failing due to temporary errors.

---

## RetryConfig Class

The `RetryConfig` class in the SDK allows you to configure the retry behavior of API requests. This class contains several key properties that control the retry logic.

### Key Properties of `RetryConfig`:

- **MaxRetries** (int): 
  - This property defines how many times the SDK should retry an API request after it fails. 
  - Default value: **3 retries**.
  - Example: If you set `MaxRetries = 5`, the SDK will attempt the request up to 5 times before failing.

- **InitialDelayMs** (int): 
  - This is the initial delay (in milliseconds) before retrying a request.
  - Default value: **1000ms (1 second)**.
  - Example: If you set `InitialDelayMs = 2000`, the SDK will wait for 2 seconds before retrying.

- **UseExponentialBackoff** (bool): 
  - This determines whether the delay between retries should increase exponentially (doubling with each attempt) or remain constant.
  - Default value: **true** (exponential backoff is used).
  - Example: If `UseExponentialBackoff = true`, the delay between retries will increase like this: 1s -> 2s -> 4s, and so on.

- **RetryStatusCodes** (HashSet<int>): 
  - This property specifies which HTTP status codes should trigger a retry. Common retryable codes include 429 (Too Many Requests), 500 (Internal Server Error), and 503 (Service Unavailable).
  - Default values: **429, 500, 503**.
  - Example: If you add 408 (Request Timeout) to this list, the SDK will retry requests that return a 408 status code.

---

## Default Retry Configuration

If you don’t provide your own retry configuration, the SDK uses a default configuration that retries 3 times, waits 1 second between retries, and uses exponential backoff.

Here is the **default retry configuration**:

```csharp
new RetryConfig
{
    MaxRetries = 3,                          // 3 retry attempts
    InitialDelayMs = 1000,                    // 1-second delay between retries
    UseExponentialBackoff = true,             // Enable exponential backoff
    RetryStatusCodes = new HashSet<int> { 429, 500, 503 }  // Retry on 429, 500, and 503 status codes
}
```

---

## Custom Retry Configuration

You can customize the retry behavior by creating a `RetryConfig` object and passing it when initializing the `GeminiApiClient` or `GeminiClient`. 

For example, if you want the SDK to retry up to 5 times with a 2-second initial delay and without exponential backoff, you can create a custom `RetryConfig` as follows:

```csharp
var retryConfig = new RetryConfig
{
    MaxRetries = 5,                          // Retry up to 5 times
    InitialDelayMs = 2000,                   // 2-second initial delay
    UseExponentialBackoff = false,           // Use fixed delay
    RetryStatusCodes = new HashSet<int> { 429, 500, 503, 408 } // Retry on additional 408 status
};

var client = new GeminiClient("YOUR_API_KEY", retryConfig: retryConfig);
```

### Important Notes:
- The `RetryConfig` can be passed to either the `GeminiApiClient` or the `GeminiClient` constructors.
- If no `RetryConfig` is provided, the default configuration is used.

---

## Retry Logic in Action

The retry logic automatically kicks in when the API returns a transient error (such as 500 or 503). Here’s how the retry mechanism works:

1. **Initial Request**: The SDK sends the API request.
2. **Error Response**: If the response is a retryable status code (e.g., 503 Service Unavailable), the SDK waits for the configured delay and then retries the request.
3. **Exponential Backoff**: If enabled, the SDK increases the delay between each retry (e.g., 1s -> 2s -> 4s).
4. **Max Retries**: The SDK will keep retrying up to the configured maximum number of attempts (`MaxRetries`). If the request still fails after the maximum retries, an exception is thrown.

---

## Example with API Call

When you make an API call using the GeminiSharp SDK, the retry logic automatically handles transient errors. Here's an example of how it works:

```csharp
try
{
    var response = await client.GenerateContentAsync("gemini-2.0-flash-exp", "Example prompt.");
}
catch (GeminiApiException ex)
{
    // Handle API-specific errors
}
catch (Exception ex)
{
    // Handle other unexpected errors
    Console.WriteLine("Request failed after retries: " + ex.Message);
}
```

In this example:
- If the API returns a transient error (such as 503), the retry logic will automatically retry the request.
- If the request still fails after the maximum retries, an exception is thrown, which you can catch and handle.

---

## Logging Retries

The SDK uses **Serilog** to log retry attempts, which helps you monitor and debug retry behavior. Each retry is logged with the delay and the error status code.

Example log output:

```
Retrying due to transient error (status 503) on attempt 2. Waiting 2000ms. Error: {"error":"Service Unavailable"}
Retrying due to transient error (status 500) on attempt 3. Waiting 4000ms. Error: {"error":"Internal Server Error"}
```

This log shows that:
- The SDK is retrying due to a **503 Service Unavailable** error.
- The delay between retries is increasing (from 2 seconds to 4 seconds).
- The retry mechanism tries again when the server is temporarily down.

---

## Conclusion

The **retry configuration** in GeminiSharp SDK is a powerful feature that helps your application handle transient errors like network issues or server overloads. It automatically retries failed requests based on configurable parameters like maximum retries, delay, and status codes to retry.

The retry logic is customizable, but by default, it retries 3 times, waits 1 second between retries, and uses exponential backoff for error handling. This feature improves the reliability and robustness of your application, especially when interacting with external APIs that may experience temporary issues.

For more details, refer to the **GeminiApiClient** and **GeminiClient** class documentation.

--- 

### Adjusting Retry Configuration for Your Needs

If you find that the default configuration doesn’t suit your needs, you can easily customize it based on the specific requirements of your application, such as:
- Adjusting the delay or number of retries based on your network reliability.
- Adding more status codes to retry on, depending on the expected types of transient errors.

The retry configuration gives you flexibility in handling errors while ensuring your application remains responsive and resilient.

---
