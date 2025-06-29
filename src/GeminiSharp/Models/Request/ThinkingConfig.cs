using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Request
{
    public class ThinkingConfig
    {
        [JsonPropertyName("thinkingBudget")]
        public int? ThinkingBudget { get; set; }

        /// <summary>
        /// Disables thinking by setting the thinking budget to zero.
        /// </summary>
        public static ThinkingConfig Disabled => new ThinkingConfig { ThinkingBudget = null };
    }
}
