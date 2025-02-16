using System.Reflection;
using System.Text.Json;

namespace GeminiSharp.Helpers
{


    public static class JsonSchemaHelper
    {
        /// <summary>
        /// Generates a JSON Schema from a given C# class.
        /// </summary>
        /// <typeparam name="T">The type to generate schema for.</typeparam>
        /// <returns>JSON Schema as a string.</returns>
        public static string GenerateSchema<T>()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var schema = new
            {
                type = "object",
                properties = properties.ToDictionary(
                    p => ConvertToCamelCase(p.Name), // Convert property names to camelCase
                    p => new { type = GetJsonType(p.PropertyType) }
                ),
                required = properties.Select(p => ConvertToCamelCase(p.Name)).ToArray()
            };

            return JsonSerializer.Serialize(schema, new JsonSerializerOptions { WriteIndented = true });
        }

        private static string GetJsonType(Type type)
        {
            if (type == typeof(string)) return "string";
            if (type == typeof(int) || type == typeof(long)) return "integer";
            if (type == typeof(double) || type == typeof(float)) return "number";
            if (type == typeof(bool)) return "boolean";
            if (type.IsArray || (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)))
                return "array";
            return "object"; // Default to object for complex types
        }

        private static string ConvertToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
