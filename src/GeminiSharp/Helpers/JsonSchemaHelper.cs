using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace GeminiSharp.Helpers
{
    /// <summary>
    /// Provides methods to generate a JSON schema from a C# class type.
    /// This helper is useful for defining the expected structure of structured outputs from the Gemini API.
    /// </summary>
    public static class JsonSchemaHelper
    {
        /// <summary>
        /// Generates a JSON schema for the specified generic type.
        /// </summary>
        /// <typeparam name="T">The type for which to generate the schema.</typeparam>
        /// <returns>An object representing the JSON schema.</returns>
        public static object GenerateSchema<T>() => GenerateSchema(typeof(T));

        /// <summary>
        /// Generates a JSON schema for the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type for which to generate the schema.</param>
        /// <returns>An object representing the JSON schema.</returns>
        private static object GenerateSchema(Type type)
        {
            if (IsNullable(type, out var underlyingNullable))
            {
                return new Dictionary<string, object>
                {
                    ["anyOf"] = new object[]
                    {
                        GenerateSchema(underlyingNullable),
                        new Dictionary<string, object> { ["type"] = "null" }
                    }
                };
            }

            if (IsPrimitive(type))
                return new { type = GetJsonType(type) };

            if (type.IsEnum)
            {
                return new JsonSchemaEnum("string", Enum.GetNames(type));
            }

            if (type.IsArray || (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)))
            {
                var elementType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault() ?? typeof(object);
                return new
                {
                    type = "array",
                    items = GenerateSchema(elementType)
                };
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var schemaProperties = new Dictionary<string, object>();
            var requiredList = new List<string>();

            foreach (var prop in properties)
            {
                try
                {
                    var propType = prop.PropertyType;
                    // Use JsonPropertyName attribute if present, otherwise convert to camelCase
                    var jsonPropertyName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? ConvertToCamelCase(prop.Name);
                    var schemaProperty = GenerateSchema(propType);

                    // Check for [Required] attribute or if it's a non-nullable value type
                    if (prop.GetCustomAttribute<RequiredAttribute>() != null || (propType.IsValueType && !IsNullable(propType, out _)))
                    {
                        requiredList.Add(jsonPropertyName);
                    }

                    // Check for [MaxLength] attribute
                    var maxLengthAttribute = prop.GetCustomAttribute<MaxLengthAttribute>();
                    if (maxLengthAttribute != null && schemaProperty is Dictionary<string, object> schemaDict && schemaDict.ContainsKey("type") && schemaDict["type"] as string == "string")
                    {
                        schemaDict["maxLength"] = maxLengthAttribute.Length;
                    }

                    schemaProperties[jsonPropertyName] = schemaProperty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating schema for property {prop.Name}: {ex.Message}");
                    schemaProperties[ConvertToCamelCase(prop.Name)] = new { type = "object", description = "Error generating schema" };
                }
            }

            return new Dictionary<string, object>
            {
                ["type"] = "object",
                ["properties"] = schemaProperties,
                ["required"] = requiredList.Any() ? requiredList : new List<string>() // Ensure 'required' is present if there are required properties
            };
        }

        /// <summary>
        /// Determines whether the specified type is nullable (i.e., Nullable&lt;T&gt;).
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="underlying">The underlying non-nullable type if applicable.</param>
        /// <returns>True if the type is nullable; otherwise, false.</returns>
        private static bool IsNullable(Type type, out Type underlying)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                underlying = Nullable.GetUnderlyingType(type)!;
                return true;
            }

            underlying = type;
            return false;
        }

        /// <summary>
        /// Determines whether the specified type is a JSON primitive type.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <returns>True if the type is a primitive type; otherwise, false.</returns>
        private static bool IsPrimitive(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            return type == typeof(string) || type == typeof(int) || type == typeof(long) ||
                   type == typeof(double) || type == typeof(float) || type == typeof(bool) ||
                   type == typeof(DateTime) || type == typeof(Guid) || type == typeof(decimal);
        }

        /// <summary>
        /// Gets the corresponding JSON schema type name for a given C# type.
        /// </summary>
        /// <param name="type">The type to map.</param>
        /// <returns>A string representing the JSON schema type.</returns>
        private static string GetJsonType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return type switch
            {
                _ when type == typeof(string) => "string",
                _ when type == typeof(int) || type == typeof(long) => "integer",
                _ when type == typeof(double) || type == typeof(float) || type == typeof(decimal) => "number",
                _ when type == typeof(bool) => "boolean",
                _ when type == typeof(DateTime) => "string", // Can add format: date-time if desired
                _ when type == typeof(Guid) => "string",
                _ => "object"
            };
        }

        /// <summary>
        /// Converts a PascalCase or TitleCase string to camelCase.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The camelCase version of the string.</returns>
        private static string ConvertToCamelCase(string input)
        {
            return string.IsNullOrEmpty(input)
                ? input
                : char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Record to represent JSON Schema enum type.
        /// </summary>
        /// <param name="type">The JSON type, usually "string".</param>
        /// <param name="enumValues">Array of enum names.</param>
        public record JsonSchemaEnum(string type, string[] @enum);
    }
}