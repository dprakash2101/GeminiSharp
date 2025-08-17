using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Embeddings
{
    /// <summary>
    /// Represents a single embedding vector.
    /// </summary>
    public class Embedding
    {
        /// <summary>
        /// Gets or sets the list of float values representing the embedding vector.
        /// </summary>
        [JsonPropertyName("value")]
        public List<float> Value { get; set; }
    }

    /// <summary>
    /// Represents the response containing embedding results from the Gemini API.
    /// </summary>
    public class EmbeddingResponse
    {
        /// <summary>
        /// Gets or sets the list of generated embeddings.
        /// </summary>
        [JsonPropertyName("embeddings")]
        public List<Embedding> Embeddings { get; set; }
    }
}