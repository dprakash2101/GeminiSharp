
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeminiSharp.Model;

namespace GeminiSharp.Util
{
    public static class PartHelper
    {
        public static Part FromText(string text)
        {
            return new Part(text: text);
        }

        public static Part FromImage(string path, string mimeType)
        {
            return new Part(inlineData: new InlineData(data: FileConverter.GetBase64String(path), mimeType: mimeType));
        }

        public static Part FromFunctionCall(FunctionCall functionCall)
        {
            return new Part(functionCall: functionCall);
        }

        public static Part FromFunctionResponse(FunctionResponse functionResponse)
        {
            return new Part(functionResponse: functionResponse);
        }

        public static Part FromSchema(Type type)
        {
            var schema = JsonSchemaGenerator.Generate(type);
            return new Part(schema: schema);
        }

        public static Part FromSchema<T>()
        {
            return FromSchema(typeof(T));
        }
    }
}
