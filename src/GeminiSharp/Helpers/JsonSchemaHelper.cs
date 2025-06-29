using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            if (IsNullableValueType(type, out var underlyingNullable))
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
                return new Dictionary<string, object> { ["type"] = GetJsonType(type) };

            if (type.IsEnum)
            {
                return new Dictionary<string, object>
                {
                    ["type"] = "string",
                    ["enum"] = Enum.GetNames(type)
                };
            }

            if (type.IsArray || (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)))
            {
                var elementType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault() ?? typeof(object);
                return new Dictionary<string, object>
                {
                    ["type"] = "array",
                    ["items"] = GenerateSchema(elementType)
                };
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var schemaProperties = new Dictionary<string, object>();
            var requiredList = new List<string>();
            var nullabilityContext = new NullabilityInfoContext();

            foreach (var prop in properties)
            {
                try
                {
                    var propType = prop.PropertyType;
                    var camelName = ConvertToCamelCase(prop.Name);
                    var schemaProperty = GenerateSchema(propType);
                    var nullabilityInfo = nullabilityContext.Create(prop);

                    // Wrap nullable reference types in anyOf to allow null
                    if (nullabilityInfo.WriteState == NullabilityState.Nullable && !IsNullableValueType(propType, out _))
                    {
                        schemaProperty = new Dictionary<string, object>
                        {
                            ["anyOf"] = new object[]
                            {
                                schemaProperty,
                                new Dictionary<string, object> { ["type"] = "null" }
                            }
                        };
                    }

                    // Handle required properties
                    if (prop.GetCustomAttribute<RequiredAttribute>() != null)
                    {
                        requiredList.Add(camelName);
                    }
                    else if (nullabilityInfo.WriteState == NullabilityState.NotNull)
                    {
                        requiredList.Add(camelName);
                    }

                    // Handle MaxLength attribute
                    var maxLengthAttribute = prop.GetCustomAttribute<MaxLengthAttribute>();
                    if (maxLengthAttribute != null && schemaProperty is Dictionary<string, object> schemaDict)
                    {
                        var targetSchema = schemaDict;
                        // If it's a nullable reference, the actual schema is inside anyOf
                        if (schemaDict.TryGetValue("anyOf", out var anyOfValue) &&
                            anyOfValue is object[] { Length: > 0 } anyOfArray &&
                            anyOfArray[0] is Dictionary<string, object> innerDict)
                        {
                            targetSchema = innerDict;
                        }

                        if (targetSchema.TryGetValue("type", out var typeValue) && typeValue as string == "string")
                        {
                            targetSchema["maxLength"] = maxLengthAttribute.Length;
                        }
                    }

                    schemaProperties[camelName] = schemaProperty;
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
                ["required"] = requiredList
            };
        }

        /// <summary>
        /// Determines whether the specified type is a nullable value type (i.e., Nullable&lt;T&gt;).
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="underlying">The underlying non-nullable type if applicable.</param>
        /// <returns>True if the type is a nullable value type; otherwise, false.</returns>
        private static bool IsNullableValueType(Type type, out Type underlying)
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
    }
}
