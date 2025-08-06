
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents the declaration of a function that the model can call.
    /// </summary>
    public class FunctionDeclaration
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A description of what the function does.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The parameters of the function.
        /// </summary>
        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }
    }
}
