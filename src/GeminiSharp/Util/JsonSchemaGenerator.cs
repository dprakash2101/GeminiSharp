
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeminiSharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace GeminiSharp.Util
{
    public static class JsonSchemaGenerator
    {
        public static JSchema Generate(Type type)
        {
            var generator = new JSchemaGenerator();
            return generator.Generate(type);
        }

        public static JSchema Generate<T>()
        {
            return Generate(typeof(T));
        }

        public static FunctionDeclaration GenerateFunctionDeclaration(Type type)
        {
            var schema = Generate(type);
            return new FunctionDeclaration(type.Name, type.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>()?.Description, schema);
        }

        public static FunctionDeclaration GenerateFunctionDeclaration<T>()
        {
            return GenerateFunctionDeclaration(typeof(T));
        }
    }
}
