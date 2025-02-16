using System.Reflection;

namespace GeminiSharp.Helpers
{
    /// <summary>
    /// Provides methods to generate a JSON schema from a C# class type.
    /// </summary>
    public static class JsonSchemaHelper
    {
        /// <summary>
        /// Generates a JSON schema for the specified generic type.
        /// </summary>
        /// <typeparam name="T">The type for which to generate the schema.</typeparam>
        /// <returns>An object representing the JSON schema.</returns>
        public static object GenerateSchema<T>()
        {
            return GenerateSchema(typeof(T));
        }

        /// <summary>
        /// Generates a JSON schema for the specified type.
        /// </summary>
        /// <param name="type">The type for which to generate the schema.</param>
        /// <returns>An object representing the JSON schema.</returns>
        private static object GenerateSchema(Type type)
        {
            if (IsPrimitive(type))
                return new { type = GetJsonType(type) };

            if (type.IsArray || (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)))
                return new { type = "array", items = GenerateSchema(type.GetElementType() ?? type.GenericTypeArguments[0]) };

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return new Dictionary<string, object>
            {
                ["type"] = "object",
                ["properties"] = properties.ToDictionary(
                    p => ConvertToCamelCase(p.Name),
                    p => GenerateSchema(p.PropertyType)
                ),
                ["required"] = properties.Select(p => ConvertToCamelCase(p.Name)).ToArray()
            };
        }

        /// <summary>
        /// Determines whether the given type is a primitive JSON type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is a primitive JSON type; otherwise, false.</returns>
        private static bool IsPrimitive(Type type)
        {
            return type == typeof(string) || type == typeof(int) || type == typeof(long) ||
                   type == typeof(double) || type == typeof(float) || type == typeof(bool);
        }

        /// <summary>
        /// Gets the corresponding JSON type for a given C# type.
        /// </summary>
        /// <param name="type">The C# type.</param>
        /// <returns>A string representing the JSON type.</returns>
        private static string GetJsonType(Type type)
        {
            return type switch
            {
                _ when type == typeof(string) => "string",
                _ when type == typeof(int) || type == typeof(long) => "integer",
                _ when type == typeof(double) || type == typeof(float) => "number",
                _ when type == typeof(bool) => "boolean",
                _ => "object"
            };
        }

        /// <summary>
        /// Converts a given string to camelCase format.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>The camelCase version of the string.</returns>
        private static string ConvertToCamelCase(string input)
        {
            return string.IsNullOrEmpty(input) ? input : char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
