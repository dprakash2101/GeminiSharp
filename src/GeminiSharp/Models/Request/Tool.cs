
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a tool that the model can use.
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// A list of function declarations.
        /// </summary>
        [JsonPropertyName("functionDeclarations")]
        public List<FunctionDeclaration> FunctionDeclarations { get; set; }
    }
}
