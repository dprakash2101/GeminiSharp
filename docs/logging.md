# 📝 Logging with Serilog in GeminiSharp

The **GeminiSharp SDK** uses **Serilog** for internal logging, providing detailed insights into API requests, responses, retries, and errors. Proper logging is crucial for debugging and monitoring your application. This guide explains how to configure and use Serilog with GeminiSharp.

---

## 🚀 Getting Started

### 1. Installation

First, make sure you have the necessary Serilog packages installed in your project. At a minimum, you'll need:

```bash
dotnet add package Serilog
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

### 2. Basic Configuration

To enable logging, you need to configure the Serilog logger at the startup of your application. Here's a typical configuration in `Program.cs`:

```csharp
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() // Capture all log levels from Debug to Fatal
            .Enrich.FromLogContext()
            .WriteTo.Console() // Output logs to the console
            .WriteTo.File("logs/geminisharp.log", rollingInterval: RollingInterval.Day) // Save logs to a file with daily rotation
            .CreateLogger();

        try
        {
            Log.Information("Starting up the application");
            // ... your application code
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
```

### 3. Integrating with Dependency Injection

If you're using dependency injection, you can register the logger with the service collection and inject it into your services.

In your `Program.cs`:

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// ... your other services
```

Then, in your `appsettings.json`:

```json
{
  "Serilog": {
    "MinimumLevel": "Debug",
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "logs/geminisharp.log",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}
```

---

## 📖 Understanding the Log Output

GeminiSharp logs detailed information about its operations. Here's an example of what you might see in your logs:

```
[14:30:15 INF] Sending request to https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent
[14:30:15 DBG] Request body: {"contents":[{"parts":[{"text":"Hello, Gemini!"}]}]}
[14:30:16 WRN] Request failed with status code TooManyRequests. Retrying in 1000ms. Attempt 1 of 3.
[14:30:17 INF] Sending request to https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent
[14:30:17 DBG] Request body: {"contents":[{"parts":[{"text":"Hello, Gemini!"}]}]}
[14:30:18 INF] Successfully received response for model gemini-1.5-flash.
```

This log output shows:
*   An informational message indicating that a request is being sent.
*   A debug message with the request body.
*   A warning message indicating that the request failed and is being retried.
*   An informational message indicating that the request was successful after a retry.

By analyzing these logs, you can gain a clear understanding of how the SDK is interacting with the Gemini API and quickly identify and troubleshoot any issues that may arise.