# 📊 Structured Output with GeminiSharp

**Structured output** is a powerful feature of the Gemini API that allows you to receive responses in a predictable, structured format, such as JSON. GeminiSharp simplifies the process of working with structured output by allowing you to define your desired structure using C# classes.

---

## ✨ Core Concepts

Instead of receiving a plain text response from the API, you can provide a JSON schema that defines the structure of the response you want. The API will then return a JSON object that conforms to this schema, which you can easily deserialize into a C# object.

---

## 🔑 Key Components

*   **`GeminiStructuredClient`**: The primary client for working with structured output. It provides methods for sending requests with a JSON schema and deserializing the response.
*   **`JsonSchemaHelper.GenerateSchema<T>()`**: A utility function that generates a JSON schema from a C# class. This eliminates the need to write JSON schemas by hand.

---

## 🚀 Getting Started

### 1. Define Your C# Model

First, create a C# class that represents the structure of the response you want to receive. For example, if you want to get information about a user, you might create a `User` class like this:

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}
```

### 2. Generate the JSON Schema

Next, use the `JsonSchemaHelper.GenerateSchema<T>()` method to generate a JSON schema from your C# class:

```csharp
var schema = JsonSchemaHelper.GenerateSchema<User>();
```

### 3. Send the Request

Now, you can send a request to the API with the generated schema. The recommended way to do this is by using the `GeminiStructuredClient`, which you can register with the dependency injection container:

```csharp
using GeminiSharp.Extensions;

builder.Services.AddGeminiClient(options =>
{
    options.ApiKey = builder.Configuration["GeminiApiKey"];
});
```

Then, you can inject the `GeminiStructuredClient` into your services or controllers and use it to send the request:

```csharp
public class UserService
{
    private readonly GeminiStructuredClient _structuredClient;

    public UserService(GeminiStructuredClient structuredClient)
    {
        _structuredClient = structuredClient;
    }

    public async Task<User> GetUserAsync(string prompt)
    {
        var schema = JsonSchemaHelper.GenerateSchema<User>();
        var response = await _structuredClient.GenerateStructuredContentAsync(
            "gemini-1.5-flash",
            prompt,
            schema
        );

        var jsonText = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;
        if (string.IsNullOrWhiteSpace(jsonText))
        {
            throw new Exception("No structured content received from the API.");
        }

        return JsonSerializer.Deserialize<User>(jsonText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
```

---

## ⚠️ Error Handling

When working with structured output, it's important to handle potential errors gracefully. Here are some common scenarios to consider:

*   **Invalid Schema**: If the JSON schema is invalid, the API will return an error.
*   **Mismatched Prompt and Schema**: If the prompt asks for information that doesn't match the schema, the API may not be able to generate a valid response.
*   **Deserialization Errors**: If the API returns a response that doesn't conform to the schema, the deserialization will fail.

By implementing robust error handling, you can ensure that your application remains stable and provides a good user experience, even when the API returns unexpected results.