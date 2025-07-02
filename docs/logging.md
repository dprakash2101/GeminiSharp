# Logging with Serilog in GeminiSharp SDK

The **GeminiSharp SDK** uses **Serilog** for logging to provide detailed information about API requests, errors, and retries. To ensure logging works effectively, make sure you configure the logger at the application startup.

---

## Logging Configuration

In your `program.cs`, you can configure the Serilog logger with multiple sinks to capture logs at various levels. Below is an example of how you can configure Serilog to log messages to the **Debug Output**, **Console**, and **File**:

### Example Logger Configuration:

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Capture Debug, Information, Warning, and Error level logs
    .WriteTo.Debug() // Send logs to Visual Studio Output Window
    .WriteTo.Console() // Optional: Output logs to the console
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) // Optional: Save logs to a file with daily rotation
    .CreateLogger();
```

### Explanation:

- **MinimumLevel.Debug()**: This sets the minimum log level to **Debug**, meaning that logs of levels **Debug**, **Information**, **Warning**, **Error**, and **Fatal** will be captured. You can adjust this depending on your needs.
  
- **WriteTo.Debug()**: This sends the log output to the **Visual Studio Output Window**, which is useful for debugging during development.
  
- **WriteTo.Console()**: This sends the log output to the **Console**, making it easier to see logs when running the application in a terminal or command prompt.
  
- **WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)**: This writes logs to a file located in the **logs** folder. The logs will roll over daily, creating a new log file every day to keep the log data manageable.

---

## Example Log Output

With this configuration, you’ll see logs in the following places:

1. **Visual Studio Output Window** (via `WriteTo.Debug()`)
2. **Console Output** (via `WriteTo.Console()`)
3. **Log File** (`logs/app.log` with daily rotation)

### Sample Logs:

```text
[10:00:00 INF] Starting application...
[10:00:01 DBG] Sending request to Gemini API for model "gemini-2.0-flash".
[10:00:05 ERR] API request failed with status code 503. Retrying...
[10:00:06 INF] Successfully generated content for model "gemini-2.0-flash".
```

---

## Using Logs to Track API Errors and Retries

Serilog captures detailed information about each retry attempt and the reason for retrying. For example:

```
Retrying due to transient error (status 503) on attempt 2. Waiting 2000ms. Error: {"error":"Service Unavailable"}
Retrying due to transient error (status 500) on attempt 3. Waiting 4000ms. Error: {"error":"Internal Server Error"}
```

This information is invaluable for debugging API issues and understanding the retry process.

---

## Conclusion

By configuring Serilog in your `program.cs`, you enable comprehensive logging for your GeminiSharp-based application. The logs will help you track API requests, errors, retries, and other important events, making it easier to monitor and troubleshoot your application.

For more information on how to configure Serilog in your application, refer to the [Serilog documentation](https://serilog.net/).
