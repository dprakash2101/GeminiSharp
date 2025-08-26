
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace GeminiSharp.Util
{
    /// <summary>
    /// A helper class to generate JSON schema from C# types.
    /// </summary>
    public static class JsonSchemaGenerator
    {
        private static readonly DefaultContractResolver _contractResolver = new DefaultContractResolver();

        /// <summary>
        /// Generates a JSON schema for the given type and returns it as a formatted string.
        /// </summary>
        /// <typeparam name="T">The type to generate the schema for.</typeparam>
        /// <returns>A JSON string representing the schema.</returns>
        public static string GenerateSchemaFor<T>()
        {
            return GenerateSchemaFor(typeof(T));
        }

        /// <summary>
        /// Generates a JSON schema for the given type and returns it as a formatted string.
        /// </summary>
        /// <param name="type">The type to generate the schema for.</param>
        /// <returns>A JSON string representing the schema.</returns>
        public static string GenerateSchemaFor(Type type)
        {
            return Generate(type).ToString(Formatting.Indented);
        }

        /// <summary>
        /// Generates a JSON schema for the given type.
        /// </summary>
        /// <param name="type">The type to generate the schema for.</param>
        /// <returns>A JObject representing the JSON schema.</returns>
        private static JObject Generate(Type type)
        {
            var schema = new JObject();
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType.IsEnum)
            {
                schema["type"] = "string";
                schema["enum"] = new JArray(Enum.GetNames(underlyingType));
                return schema;
            }

            var contract = _contractResolver.ResolveContract(underlyingType);

            switch (contract)
            {
                case JsonPrimitiveContract primitiveContract:
                    schema["type"] = GetPrimitiveType(primitiveContract.TypeCode);
                    break;

                case JsonArrayContract arrayContract:
                    schema["type"] = "array";
                    if (arrayContract.CollectionItemType != null)
                    {
                        schema["items"] = Generate(arrayContract.CollectionItemType);
                    }
                    break;

                case JsonObjectContract objectContract:
                    schema["type"] = "object";
                    var properties = new JObject();
                    var required = new JArray();

                    foreach (var prop in objectContract.Properties)
                    {
                        if (prop.Ignored || !prop.Readable) continue;

                        var propertySchema = Generate(prop.PropertyType);

                        var description = prop.AttributeProvider?.GetAttributes(typeof(DescriptionAttribute), true)
                            .FirstOrDefault() as DescriptionAttribute;
                        if (description != null)
                        {
                            propertySchema["description"] = description.Description;
                        }

                        properties[prop.PropertyName] = propertySchema;

                        if (prop.Required == Required.Always || prop.Required == Required.AllowNull)
                        {
                            required.Add(prop.PropertyName);
                        }
                    }
                    schema["properties"] = properties;
                    if (required.Count > 0)
                    {
                        schema["required"] = required;
                    }
                    break;

                case JsonDictionaryContract dictionaryContract:
                    schema["type"] = "object";
                    if (dictionaryContract.DictionaryValueType != null)
                    {
                        schema["additionalProperties"] = Generate(dictionaryContract.DictionaryValueType);
                    }
                    break;
            }

            return schema;
        }

        private static string GetPrimitiveType(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return "boolean";
                case TypeCode.String:
                case TypeCode.Char:
                case TypeCode.DateTime:
                case TypeCode.DateTimeOffset:
                case TypeCode.Guid:
                case TypeCode.TimeSpan:
                case TypeCode.Uri:
                    return "string";
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return "integer";
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "number";
                default:
                    return "string"; // Default to string for other types
            }
        }
    }
}
