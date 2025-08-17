using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Response
{
    /// <summary>
    /// Represents the response containing the token count for a given input.
    /// </summary>
    public class CountTokensResponse
    {
        /// <summary>
        /// Gets or sets the total number of tokens in the input.
        /// </summary>
        [JsonPropertyName("totalTokens")]
        public int TotalTokens { get; set; }
    }
}