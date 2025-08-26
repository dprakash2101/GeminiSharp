# JSON Schema Generation

The `GeminiSharp.Util.JsonSchemaGenerator` is a utility class that allows you to generate a JSON schema from any C# class. This is particularly useful for providing function and tool definitions to the Gemini API.

The generator respects `Newtonsoft.Json` attributes (like `JsonPropertyAttribute` and `JsonIgnoreAttribute`) and `DescriptionAttribute` to create a schema that accurately represents how your model will be serialized.

## Usage

To generate a schema, you can use the static `GenerateSchemaFor<T>()` or `GenerateSchemaFor(Type type)` methods.

### Example

Given the following C# class:

```csharp
using System.ComponentModel;
using Newtonsoft.Json;

public class MyCustomType
{
    [JsonProperty("user_name")]
    [Description("The name of the user.")]
    public string UserName { get; set; }

    [JsonProperty("user_age")]
    [Description("The age of the user.")]
    public int? UserAge { get; set; }

    [JsonIgnore]
    public string InternalNotes { get; set; }
}
```

You can generate a JSON schema for it like this:

```csharp
using GeminiSharp.Util;

// ...

string schema = JsonSchemaGenerator.GenerateSchemaFor<MyCustomType>();

Console.WriteLine(schema);
```

### Output

The generated schema will be a JSON string similar to this:

```json
{
  "type": "object",
  "properties": {
    "user_name": {
      "type": "string",
      "description": "The name of the user."
    },
    "user_age": {
      "type": "integer",
      "description": "The age of the user."
    }
  }
}
```
Notice that the `InternalNotes` property was ignored because of the `[JsonIgnore]` attribute, and the property names and descriptions from the attributes were used. Nullable properties like `int?` are not marked as required by default.
