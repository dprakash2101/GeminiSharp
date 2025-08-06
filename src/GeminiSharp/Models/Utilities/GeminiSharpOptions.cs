namespace GeminiSharp.Models.Utilities
{
    /// <summary>
    /// Options class for configuring the GeminiSharp SDK.
    /// </summary>
    public class GeminiSharpOptions
    {
        /// <summary>
        /// Gets or sets the API key for authentication with the Gemini API.
        /// This is a required setting.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the base URL of the Gemini API. If not specified, a default Google endpoint will be used.
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the retry configuration for API requests. If not specified, a default retry policy will be used.
        /// </summary>
        public RetryConfig? RetryConfig { get; set; }
    }
}