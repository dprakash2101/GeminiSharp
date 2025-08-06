# 📞 Function Calling with GeminiSharp

GeminiSharp's function calling capability allows the Gemini model to identify when a user's intent can be fulfilled by calling a tool (function) that you provide. The model doesn't execute the function itself, but rather returns a structured JSON object indicating the function name and its arguments. Your application then executes the function and returns the result to the model for further processing.

---

## 🔑 Key Concepts

*   **`Tool`**: Represents a collection of functions that the model can call. Each `Tool` contains a list of `FunctionDeclaration` objects.
*   **`FunctionDeclaration`**: Describes a function that the model can invoke. It includes the function's `name`, `description`, and `parameters` (defined using a JSON schema).
*   **`GenerateContentWithFunctionCallingAsync`**: The method in `UtilityClient` (accessed via `IGeminiClient`) that enables the model to suggest function calls based on the prompt.
*   **`FunctionCall`**: The structured output from the model when it determines a function needs to be called. It contains the `name` of the function to call and its `args` (arguments).

---

## 🧑‍💻 C# Example: Implementing Function Calling

This example demonstrates how to define a tool with a function, send a prompt that triggers the function call, and then execute the function based on the model's response.

### **Step 1: Define Your Function(s)**

First, define the C# methods that represent the functions you want the model to be able to call. These should be static or part of a class you can instantiate.

```csharp
public class WeatherService
{
    public static string GetCurrentWeather(string location, string unit = "celsius")
    {
        // In a real application, this would call an external weather API
        if (location.Equals("London", StringComparison.OrdinalIgnoreCase))
        {
            return $"The current weather in {location} is 15 degrees {unit} and cloudy.";
        }
        else if (location.Equals("New York", StringComparison.OrdinalIgnoreCase))
        {
            return $"The current weather in {location} is 25 degrees {unit} and sunny.";
        }
        else
        {
            return $"Could not retrieve weather for {location}.";
        }
    }

    public static string GetWeatherForecast(string location, int days, string unit = "celsius")
    {
        // In a real application, this would call an external weather API for forecast
        if (location.Equals("London", StringComparison.OrdinalIgnoreCase))
        {
            return $"The {days}-day forecast for {location} is mostly rain with temperatures around 10 degrees {unit}.";
        }
        else if (location.Equals("New York", StringComparison.OrdinalIgnoreCase))
        {
            return $"The {days}-day forecast for {location} is clear skies with temperatures around 28 degrees {unit}.";
        }
        else
        {
            return $"Could not retrieve forecast for {location}.";
        }
    }
}
```

### **Step 2: Declare Your Functions as Tools**

Use `FunctionDeclaration` to describe your C# functions to the Gemini model. The `parameters` should be a JSON schema defining the function's arguments.

```csharp
using GeminiSharp.Models.Request;
using GeminiSharp.Helpers;
using System.Collections.Generic;

// Declare the GetCurrentWeather function
var getCurrentWeatherFunction = new FunctionDeclaration
{
    Name = "GetCurrentWeather",
    Description = "Gets the current weather in a given location",
    Parameters = JsonSchemaHelper.GenerateSchema(new
    {
        location = "", // Example for schema generation
        unit = "celsius" // Example for schema generation
    })
};

// Declare the GetWeatherForecast function
var getWeatherForecastFunction = new FunctionDeclaration
{
    Name = "GetWeatherForecast",
    Description = "Gets the weather forecast for a given location and number of days",
    Parameters = JsonSchemaHelper.GenerateSchema(new
    {
        location = "",
        days = 0,
        unit = "celsius"
    })
};

// Create a Tool containing your functions
var weatherTool = new Tool
{
    FunctionDeclarations = new List<FunctionDeclaration>
    {
        getCurrentWeatherFunction,
        getWeatherForecastFunction
    }
};

var tools = new List<Tool> { weatherTool };
```

### **Step 3: Send Prompt and Handle Function Calls**

```csharp
using GeminiSharp.Client;
using GeminiSharp.API;
using GeminiSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

public class FunctionCallingExample
{
    public static async Task Run(string apiKey)
    {
        // 1. Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddGeminiSharp(apiKey) // Replace with your actual API key
            .BuildServiceProvider();

        // 2. Resolve IGeminiClient from the container
        var geminiClient = serviceProvider.GetRequiredService<IGeminiClient>();

        string model = "gemini-2.5-flash"; // Model capable of function calling

        // Define your tools (as shown in Step 2)
        // ... (code for defining weatherTool and tools list)

        Console.WriteLine($"\n--- Function Calling Example (Model: {model}) ---\n");

        string prompt = "What's the weather like in London?";
        // string prompt = "What's the 3-day forecast for New York in fahrenheit?";

        Console.WriteLine($"User: {prompt}");

        try
        {
            var response = await geminiClient.GenerateContentWithFunctionCallingAsync(model, prompt, tools);

            var functionCall = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.FunctionCall;

            if (functionCall != null)
            {
                Console.WriteLine($"Model wants to call function: {functionCall.Name}");
                Console.WriteLine($"Arguments: {JsonSerializer.Serialize(functionCall.Args)}");

                // Execute the function based on the model's suggestion
                string functionResult = string.Empty;
                if (functionCall.Name == "GetCurrentWeather")
                {
                    // Deserialize arguments to a dynamic object or a specific class
                    dynamic args = functionCall.Args;
                    string location = args.location;
                    string unit = args.unit ?? "celsius"; // Handle optional unit
                    functionResult = WeatherService.GetCurrentWeather(location, unit);
                }
                else if (functionCall.Name == "GetWeatherForecast")
                {
                    dynamic args = functionCall.Args;
                    string location = args.location;
                    int days = args.days;
                    string unit = args.unit ?? "celsius";
                    functionResult = WeatherService.GetWeatherForecast(location, days, unit);
                }
                else
                {
                    functionResult = $"Unknown function: {functionCall.Name}";
                }

                Console.WriteLine($"Function Result: {functionResult}");

                // Optionally, send the function result back to the model for further conversation
                // This is crucial for multi-turn function calling
                // var followUpResponse = await geminiClient.GenerateContentAsync(
                //     model,
                //     $"The function {functionCall.Name} returned: {functionResult}"
                // );
                // Console.WriteLine($"Model's follow-up: {followUpResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text}");
            }
            else
            {
                // Model returned text, not a function call
                Console.WriteLine($"Model: {response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text}");
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error during function calling: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred during function calling: {ex.Message}");
        }
    }
}

// To run this example:
// FunctionCallingExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## **🎯 Benefits of Function Calling**

✅ **Extend Model Capabilities**: Allow the Gemini model to interact with external systems and real-world data.
✅ **Complex Task Automation**: Enable the model to break down complex user requests into actionable function calls.
✅ **Enhanced User Experience**: Provide more dynamic and contextually relevant responses by integrating live data or actions.
✅ **Structured Interaction**: The model provides structured `FunctionCall` objects, making it easy to parse and execute the suggested actions.

---

## **📌 Important Considerations**

*   **Function Execution**: The model *suggests* function calls; your application is responsible for *executing* them and optionally feeding the results back to the model.
*   **Schema Accuracy**: Ensure your `FunctionDeclaration` parameters accurately reflect the actual arguments of your C# functions. Inaccurate schemas can lead to incorrect function calls from the model.
*   **Security**: Be cautious about what functions you expose to the model, especially if they perform sensitive operations. Implement proper validation and authorization around function execution.
*   **Error Handling**: Handle cases where the model suggests a non-existent function or provides invalid arguments.
*   **Multi-Turn Interactions**: For complex workflows, you might need to send the function's output back to the model as part of a multi-turn conversation to guide its next steps.

---
