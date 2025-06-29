# ⚙️ Retry Configuration in GeminiSharp

The **GeminiSharp SDK** includes a robust and configurable retry mechanism to handle transient errors that can occur when communicating with the Gemini API. This guide explains how to configure the retry behavior to improve the reliability of your application.

---

## ✨ Core Concepts

Transient errors are temporary issues, such as network interruptions or temporary server unavailability, that are likely to be resolved by simply retrying the request. GeminiSharp uses the **Polly** library to implement a sophisticated retry policy that can automatically handle these errors for you.

---

## 🔑 Key Components

The retry mechanism is configured using the `RetryConfiguration` class, which allows you to customize the following options:

*   **`MaxRetryAttempts`**: The maximum number of times to retry a failed request. The default is `3`.
*   **`Delay`**: The delay between retry attempts. The default is 1 second.
*   **`BackoffType`**: The type of backoff to use between retries. The options are:
    *   `Linear`: The delay remains constant for each retry.
    *   `Exponential`: The delay increases exponentially with each retry. This is the default.
*   **`UseJitter`**: Whether to add a random amount of jitter to the delay. This can help prevent a "thundering herd" of retries from overwhelming the server. The default is `true`.

---

## 🚀 Getting Started

### 1. Default Configuration

By default, GeminiSharp is configured to retry failed requests up to 3 times with an exponential backoff, starting with a 1-second delay. This is a sensible default for most applications.

### 2. Customizing the Retry Configuration

You can customize the retry behavior by providing a `RetryConfiguration` object when you register the Gemini clients with the dependency injection container.

In your `Program.cs` or `Startup.cs`:

```csharp
using GeminiSharp.Extensions;
using GeminiSharp.Models.Configuration;
using Polly;

builder.Services.AddGeminiClient(options =>
{
    options.ApiKey = builder.Configuration["GeminiApiKey"];
    options.RetryConfiguration = new RetryConfiguration
    {
        MaxRetryAttempts = 5,
        Delay = TimeSpan.FromSeconds(2),
        BackoffType = DelayBackoffType.Linear,
        UseJitter = false
    };
});
```

In this example, we've configured the client to:
*   Retry up to 5 times.
*   Use a fixed delay of 2 seconds between retries.
*   Disable jitter.

### 3. Disabling Retries

If you want to disable the retry mechanism entirely, you can set `MaxRetryAttempts` to `0`:

```csharp
options.RetryConfiguration = new RetryConfiguration
{
    MaxRetryAttempts = 0
};
```

---

## 📖 Understanding the Retry Logic

When a request fails with a transient error (such as a `5xx` status code or a `HttpRequestException`), the retry mechanism kicks in and waits for the configured delay before retrying the request. If the request continues to fail, the delay will be adjusted based on the configured backoff type, and the request will be retried until the maximum number of retry attempts is reached.

If the request still fails after all retry attempts have been exhausted, a `GeminiApiException` will be thrown, which you can catch and handle in your code.

By customizing the retry configuration, you can fine-tune the behavior of the SDK to meet the specific needs of your application and ensure that it remains resilient in the face of transient errors.