
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents a function call that the model wants to make.
    /// </summary>
    public class FunctionCall
    {
        /// <summary>
        /// The name of the function to call.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The arguments to the function.
        /// </summary>
        [JsonPropertyName("args")]
        public JsonElement Args { get; set; }
    }
}
