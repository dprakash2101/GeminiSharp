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
                return new
                {
                    type = "string",
                    @enum = Enum.GetNames(type)
                };
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
                var propType = prop.PropertyType;
                var camelName = ConvertToCamelCase(prop.Name);
                schemaProperties[camelName] = GenerateSchema(propType);

                if (!IsNullable(propType, out _))
                    requiredList.Add(camelName);
            }

            return new Dictionary<string, object>
            {
                ["type"] = "object",
                ["properties"] = schemaProperties,
                ["required"] = requiredList
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
                   type == typeof(double) || type == typeof(float) || type == typeof(bool);
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
                _ when type == typeof(double) || type == typeof(float) => "number",
                _ when type == typeof(bool) => "boolean",
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
    }
}
