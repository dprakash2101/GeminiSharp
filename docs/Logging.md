# Logging

The GeminiSharp client uses [Serilog](https://serilog.net/) for structured logging, allowing you to see detailed information about the API requests being made. Logging is disabled by default and can be enabled by providing a logger instance.

## How to Enable Logging

To enable logging, you need to create a `Serilog.ILogger` instance and assign it to the `Logger` property of the `Configuration` object that you provide to the `GeminiApi`.

### Prerequisites

You will need to add the `Serilog` NuGet package to your project, along with any sinks you wish to use (e.g., `Serilog.Sinks.Console`).

```bash
dotnet add package Serilog
dotnet add package Serilog.Sinks.Console
```

### Example: Logging to the Console

Here is an example of how to configure a simple logger that writes to the console and enable it in the `GeminiApi`.

```csharp
using GeminiSharp.Api;
using GeminiSharp.Client;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Create a Serilog logger
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        // 2. Create a new configuration object
        var config = new Configuration();

        // 3. Assign the logger to the configuration
        config.Logger = Log.Logger;
        
        // You can also set the logger on the global configuration
        // GlobalConfiguration.Instance.Logger = Log.Logger;

        // 4. Initialize the GeminiApi with the configuration
        var apiInstance = new GeminiApi(config);

        // Now, all calls made with this apiInstance will be logged.
        try
        {
            // ... make an API call ...
            // apiInstance.GenerateContent(...);
        }
        catch (ApiException e)
        {
            // Exceptions will also be logged
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
```

By providing a logger instance, you can gain insight into the requests, responses, and any potential errors encountered by the client, which is invaluable for debugging.
