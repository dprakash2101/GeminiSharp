# **Structured Output in GeminiSharp**

Structured output in GeminiSharp allows you to define a precise format for the Gemini API's responses by converting your **C# models into JSON schemas**. This feature ensures you receive predictable and well-formed data, making it easier to integrate AI-generated content directly into your application's logic.

---

## **🚀 How It Works**

1.  **Define a C# class** that represents the desired structure of the API's response.
2.  **Generate a JSON schema** from this C# class using `JsonSchemaHelper.GenerateSchema<T>()`.
3.  **Send this schema along with your prompt** to the Gemini API via the `StructuredContentClient`.
4.  The API attempts to generate content that adheres to the provided schema.
5.  **Deserialize the API's response** into a strongly-typed C# object for easy consumption.

---

## **📌 Generating a JSON Schema**

The `JsonSchemaHelper` utility simplifies the process of creating a JSON schema from your C# models. This schema is then sent to the Gemini API to guide its output.

```csharp
using GeminiSharp.Helpers;

// Define your C# model (example below)
public class MyStructuredData
{
    public string? Field1 { get; set; }
    public int Field2 { get; set; }
}

// Generate the JSON schema
var schema = JsonSchemaHelper.GenerateSchema<MyStructuredData>();

// The 'schema' object can now be passed to the Gemini API
```

---

## **🔹 Example Usage in a C# Console Application**

This example demonstrates how to define a C# model, generate its JSON schema, and then use the `StructuredContentClient` (accessed via `GeminiClient`) to get a structured response from the Gemini API.

### **Step 1: Define the C# Model(s)**

Create C# classes that represent the structure you expect from the Gemini API. This example uses `PlayerStats` and `MatchStats`.

```csharp
public class PlayerStats
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public MatchStats? ODI { get; set; }
    public MatchStats? Test { get; set; }
    public MatchStats? T20 { get; set; }
    public MatchStats? Domestic { get; set; }
}

public class MatchStats
{
    public int Matches { get; set; }
    public int Runs { get; set; }
    public int Hundreds { get; set; }
    public int Fifties { get; set; }
}
```

### **Step 2: Send the Request and Process the Response**

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using GeminiSharp.Models.Request;

public class StructuredOutputExample
{
    public static async Task Run(string apiKey)
    {
        // Initialize GeminiClient (which provides access to StructuredContentClient)
        using var httpClient = new HttpClient();
        var geminiClient = new GeminiClient(apiKey, httpClient);

        // 1. Generate the JSON schema from your C# model
        var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();

        string prompt = "Provide cricket player stats for Virat Kohli";
        Console.WriteLine($"Requesting structured data for: \"{prompt}\"");

        try
        {
            // 2. Call GenerateStructuredContentAsync using the GeminiClient facade
            // You can pass null for the model to use the default (gemini-2.5-flash)
            var response = await geminiClient.GenerateStructuredContentAsync(
                null, // Use default model (gemini-2.5-flash)
                prompt,
                schema,
                new GenerationConfig { response_mime_type = "application/json" } // Ensure JSON output
            );

            // 3. Extract the JSON string from the response
            var jsonText = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            if (string.IsNullOrWhiteSpace(jsonText))
            {
                Console.WriteLine("Error: No structured content received from the API.");
                return;
            }

            // 4. Deserialize the JSON string back into your C# model
            PlayerStats? structuredData = JsonSerializer.Deserialize<PlayerStats>(jsonText, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Important for matching JSON to C# properties
                WriteIndented = true
            });

            if (structuredData != null)
            {
                Console.WriteLine("\nSuccessfully received structured data:");
                Console.WriteLine($"Player Name: {structuredData.Name}");
                Console.WriteLine($"ODI Matches: {structuredData.ODI?.Matches}, Runs: {structuredData.ODI?.Runs}");
                Console.WriteLine($"Test Matches: {structuredData.Test?.Matches}, Runs: {structuredData.Test?.Runs}");
                // ... print other stats
            }
            else
            {
                Console.WriteLine("Failed to deserialize structured data.");
            }
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message} (Status Code: {ex.StatusCode})");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}

// To run this example:
// StructuredOutputExample.Run("YOUR_GEMINI_API_KEY").Wait();
```

---

## **🎯 Benefits of Structured Output**

✅ **Automated Schema Generation** — No need to manually write complex JSON schemas; `JsonSchemaHelper` does the heavy lifting.
✅ **Predictable API Responses** — Guarantees that the API's output adheres to a predefined structure, simplifying parsing and integration.
✅ **Strongly-Typed Data** — Directly deserialize API responses into C# objects, leveraging compile-time type checking and reducing runtime errors.
✅ **Supports Complex Objects** — Works seamlessly with nested objects and collections within your C# models.
✅ **Seamless Integration** — Designed to work effortlessly with the Gemini API's structured content capabilities.

---

## **📌 Next Steps**

*   Explore more complex C# models with various data types and nesting levels.
*   Implement validation logic for the deserialized structured data.
*   Consider using `System.Text.Json.Serialization` attributes for more control over JSON serialization/deserialization.

---