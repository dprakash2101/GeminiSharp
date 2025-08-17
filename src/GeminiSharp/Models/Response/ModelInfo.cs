
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Contains information about a specific model.
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// The resource name of the model, e.g., "models/gemini-1.5-pro-latest".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The version number of the model.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// The human-readable name of the model, e.g., "Gemini 1.5 Flash".
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// A short description of the model's capabilities.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The maximum number of input tokens allowed.
        /// </summary>
        [JsonPropertyName("inputTokenLimit")]
        public int InputTokenLimit { get; set; }

        /// <summary>
        /// The maximum number of output tokens the model can generate.
        /// </summary>
        [JsonPropertyName("outputTokenLimit")]
        public int OutputTokenLimit { get; set; }

        /// <summary>
        /// The API methods the model supports, e.g., "generateContent".
        /// </summary>
        [JsonPropertyName("supportedGenerationMethods")]
        public string[] SupportedGenerationMethods { get; set; }

        /// <summary>
        /// Controls the randomness of the output.
        /// </summary>
        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        /// <summary>
        /// The maximum cumulative probability of tokens to consider when sampling.
        /// </summary>
        [JsonPropertyName("topP")]
        public float? TopP { get; set; }

        /// <summary>
        /// The maximum number of tokens to consider when sampling.
        /// </summary>
        [JsonPropertyName("topK")]
        public int? TopK { get; set; }
    }
}
