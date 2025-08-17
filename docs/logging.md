# Logging with Serilog in GeminiSharp SDK

The **GeminiSharp SDK** leverages **Serilog** for comprehensive logging, providing insights into API requests, responses, errors, and retry attempts. Proper logger configuration at your application's startup is crucial to utilize this feature effectively.

---

## Configuring Serilog

To enable logging, configure Serilog in your application's entry point (e.g., `Program.cs`). You can direct logs to various sinks such as the Debug Output, Console, or a file.

### Example Serilog Configuration:

```csharp
using Serilog;
using Serilog.Events;

public class Program
{
    public static void Main(string[] args)
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() // Set the minimum level to capture (Debug, Information, Warning, Error, Fatal)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information) // Suppress verbose Microsoft logs
            .Enrich.FromLogContext()
            .WriteTo.Console() // Output logs to the console
            .WriteTo.Debug() // Output logs to Visual Studio's Debug Output window
            .WriteTo.File("logs/geminisharp-.txt", rollingInterval: RollingInterval.Day) // Log to a daily rolling file
            .CreateLogger();

        try
        {
            Log.Information("Starting GeminiSharp application...");
            // Your application's main logic here
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush(); // Ensure all buffered logs are written
        }
    }
}
```

### Explanation of Configuration:

*   **`.MinimumLevel.Debug()`**: Captures all log events from `Debug` level upwards (Debug, Information, Warning, Error, Fatal). Adjust this level based on the verbosity you need.
*   **`.MinimumLevel.Override("Microsoft", LogEventLevel.Information)`**: Prevents excessive logging from Microsoft-related namespaces, keeping your logs cleaner.
*   **`.Enrich.FromLogContext()`**: Enriches log events with properties from the Serilog `LogContext`, useful for adding contextual information (e.g., request IDs in web applications).
*   **`.WriteTo.Console()`**: Displays log messages directly in your application's console window.
*   **`.WriteTo.Debug()`**: Sends logs to the debug output, visible in IDEs like Visual Studio.
*   **`.WriteTo.File("logs/geminisharp-.txt", rollingInterval: RollingInterval.Day)`**: Configures logging to a file. Logs will be written to `logs/geminisharp-YYYYMMDD.txt`, with a new file created each day.
*   **`Log.CloseAndFlush()`**: Important for console applications to ensure all pending log events are written before the application exits.

---

## Understanding GeminiSharp Log Outputs

GeminiSharp emits informative log messages at various stages of its operation, including API calls, successful responses, and error handling (especially during retries).

### Sample Log Entries:

```text
[INF] Initializing GeminiSharp client with base URL: https://generativelanguage.googleapis.com/
[INF] Generating content for model gemini-2.5-flash with prompt: "Tell me a story."
[DBG] Sending HTTP POST request to /v1beta/models/gemini-2.5-flash:generateContent
[DBG] Request Body: {"contents":[{"parts":[{"text":"Tell me a story."}]}]}
[DBG] Received HTTP 200 OK from /v1beta/models/gemini-2.5-flash:generateContent
[INF] Successfully generated content for model gemini-2.5-flash.
[WRN] API call failed with status code 503 Service Unavailable. Retrying in 2.5s (Attempt 1/3).
[ERR] API error while generating content for model gemini-2.5-flash. Error: Service Unavailable
[FTL] Application terminated unexpectedly
```

### Key Log Messages:

*   **`[INF] Initializing GeminiSharp client...`**: Indicates successful client setup.
*   **`[INF] Generating content for model {Model} with prompt: {Prompt}`**: Logs the start of a content generation request.
*   **`[DBG] Sending HTTP POST request to {Path}`**: Details the outgoing HTTP request (visible at `Debug` level).
*   **`[DBG] Request Body: {Body}`**: Shows the full request payload (sensitive information might be present, handle with care).
*   **`[DBG] Received HTTP {StatusCode} {StatusPhrase} from {Path}`**: Confirms the HTTP response received.
*   **`[INF] Successfully generated content for model {Model}.`**: Confirms a successful API call.
*   **`[WRN] API call failed with status code {StatusCode} {StatusPhrase}. Retrying...`**: Alerts to transient errors and retry attempts (part of the built-in retry policy).
*   **`[ERR] API error while generating content for model {Model}. Error: {ErrorMessage}`**: Logs non-recoverable API errors or final failures after retries.
*   **`[FTL] Application terminated unexpectedly`**: Indicates a critical, unhandled exception.

---

## Conclusion

By integrating and configuring Serilog, you gain a powerful tool for monitoring and debugging your GeminiSharp-powered applications. The detailed logs provide crucial visibility into the SDK's operations, helping you diagnose issues and ensure the smooth functioning of your AI integrations.

For more advanced Serilog configurations and sinks, refer to the [official Serilog documentation](https://serilog.net/).