# **Structured Output in GeminiSharp**
Structured output in GeminiSharp allows users to define a structured response format by converting their **C# models into JSON schema**. This feature ensures **predictable and well-formed responses** when interacting with the **Gemini API**.

---

## **🚀 How It Works**
1. **Define a C# class** representing the expected structured response.  
2. **Generate a JSON schema** using `JsonSchemaHelper`.  
3. **Send the schema along with the prompt** to the Gemini API.  
4. **Deserialize the response** into a strongly typed C# object.

---

## **📌 Generating a JSON Schema**
You can generate a **JSON schema** from any C# model using `JsonSchemaHelper`:

```csharp
var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();
```

This will automatically create a JSON schema based on the **`PlayerStats`** class, ensuring that the Gemini API returns responses in the correct structure.

---

## **🔹 Example Usage in C# Console Application**
### **Step 1: Define the C# Model**
```csharp
public class PlayerStats
{
    public string Name { get; set; }
    public int Age { get; set; }
    public MatchStats ODI { get; set; }
    public MatchStats Test { get; set; }
    public MatchStats T20 { get; set; }
    public MatchStats Domestic { get; set; }
}

public class MatchStats
{
    public int Matches { get; set; }
    public int Runs { get; set; }
    public int Hundreds { get; set; }
    public int Fifties { get; set; }
}
```

### **Step 2: Convert C# Model to JSON Schema**
```csharp
var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();
```
This will generate a **JSON schema** like this:
```json
{
  "type": "object",
  "properties": {
    "name": { "type": "string" },
    "age": { "type": "integer" },
    "oDI": {
      "type": "object",
      "properties": {
        "matches": { "type": "integer" },
        "runs": { "type": "integer" },
        "hundreds": { "type": "integer" },
        "fifties": { "type": "integer" }
      },
      "required": ["matches", "runs", "hundreds", "fifties"]
    }
  },
  "required": ["name", "age", "oDI", "test", "t20", "domestic"]
}
```

### **Step 3: Send the Request**
```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using GeminiSharp.Client;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        using var httpClient = new HttpClient();
        var apiKey = "your-gemini-api-key"; // Replace with actual API key
        var geminiClient = new GeminiClient(httpClient, apiKey);

        try
        {
            string model = "gemini-2.0";
            var schema = JsonSchemaHelper.GenerateSchema<PlayerStats>();
            
            var response = await geminiClient.GenerateStructuredContentAsync<PlayerStats>(
                model, "Provide cricket player stats for Virat Kohli", schema);
            
            Console.WriteLine($"Player Name: {response.Name}, Runs in ODI: {response.ODI.Runs}");
        }
        catch (GeminiApiException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

---

## **🛠️ ASP.NET Web API Implementation**
This example shows how to use **structured output** in an **ASP.NET Core Web API**.

### **📌 Controller Code**
```csharp
using GeminiSdkTest.Models;
using GeminiSharp.API;
using GeminiSharp.Client;
using GeminiSharp.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace geminisdktest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiSDKStructuredController : ControllerBase
    {
        public class GenerateStructuredRequest
        {
            public string Prompt { get; set; } = string.Empty;
        }

        [HttpPost("generate-structured")]
        public async Task<IActionResult> GenerateStructuredPlayerStats(
            [FromBody] GenerateStructuredRequest request,
            [FromHeader(Name = "GeminiApiKey")] string apiKey,
            [FromHeader(Name = "Gemini-Model")] string? model)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest(new { error = "Prompt cannot be empty." });

            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest(new { error = "API key is required." });

            model ??= "gemini-1.5-flash"; // Default model

            using var httpClient = new HttpClient();
            var geminiClient = new GeminiStructuredClient(httpClient, apiKey);

            try
            {
                // Generate JSON schema for structured output
                object jsonSchema = JsonSchemaHelper.GenerateSchema<PlayerStats>();

                var response = await geminiClient.GenerateStructuredContentAsync(
                    model: model,
                    prompt: request.Prompt,
                    jsonSchema: jsonSchema
                );

                var jsonText = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

                if (string.IsNullOrWhiteSpace(jsonText))
                {
                    return BadRequest(new { error = "No valid structured content received from Gemini API." });
                }

                // Deserialize the structured data into PlayerStats
                PlayerStats? structuredData;
                try
                {
                    structuredData = JsonSerializer.Deserialize<PlayerStats>(jsonText, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                catch (JsonException jsonEx)
                {
                    return BadRequest(new { error = "Invalid structured response format.", details = jsonEx.Message });
                }

                return Ok(structuredData);
            }
            catch (GeminiApiException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
        }
    }
}
```

---

## **🎯 API Usage**
### **📌 Request**
```http
POST /api/GeminiSDKStructured/generate-structured HTTP/1.1
Host: your-api-url
Content-Type: application/json
GeminiApiKey: your-gemini-api-key
Gemini-Model: gemini-1.5-flash

{
    "prompt": "Provide cricket player stats for Virat Kohli"
}
```

### **📌 Response**
```json
{
    "name": "Virat Kohli",
    "age": 35,
    "oDI": {
        "matches": 292,
        "runs": 13848,
        "hundreds": 50,
        "fifties": 65
    },
    "test": {
        "matches": 113,
        "runs": 8848,
        "hundreds": 29,
        "fifties": 30
    },
    "t20": {
        "matches": 115,
        "runs": 4008,
        "hundreds": 1,
        "fifties": 37
    },
    "domestic": {
        "matches": 272,
        "runs": 12650,
        "hundreds": 41,
        "fifties": 92
    }
}
```

---

## **🎯 Benefits**
✅ **Automated Schema Generation** — No need to manually write JSON schemas.  
✅ **Predictable API Responses** — Ensures structured and well-formed responses.  
✅ **Supports Nested Objects** — Works with complex objects and collections.  
✅ **Seamless Integration** — Works with Gemini API and structured content requests.  

---

## **📌 Next Steps**
🔹 Add **unit tests** to validate schema generation.  
🔹 Explore **support for additional serialization attributes**.  
🔹 Extend support for **other structured data types**.  

---

This documentation provides **end-to-end** guidance on **structured output** in **GeminiSharp** for **C# and ASP.NET Web API**. 🚀 Let me know if you need further refinements!
