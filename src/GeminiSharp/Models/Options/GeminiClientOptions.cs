namespace GeminiSharp.Models.Options
{
    /// <summary>
    /// Represents configuration options for the Gemini API client.
    /// </summary>
    /// <remarks>
    /// These options are typically loaded from appsettings.json via Dependency Injection (DI) 
    /// in ASP.NET Core using the "Gemini" configuration section.
    /// </remarks>
    public class GeminiClientOptions
    {
        /// <summary>
        /// Gets or sets the API key used to authenticate requests to the Gemini API.
        /// </summary>
        /// <remarks>
        /// This property is required and must be provided in the configuration. 
        /// Defaults to an empty string if not specified, but an empty value will cause an exception during client initialization.
        /// </remarks>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the base URL for the Gemini API.
        /// </summary>
        /// <remarks>
        /// Defaults to "https://generativelanguage.googleapis.com/v1beta/models/" if not specified. 
        /// Use this to override the API endpoint if needed (e.g., for testing or alternative environments).
        /// </remarks>
        public string BaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models/";

        /// <summary>
        /// Gets or sets the default model to use for content generation.
        /// </summary>
        /// <remarks>
        /// Defaults to "gemini-1.5-flash" if not specified in the configuration. 
        /// This can be overridden by the client implementation (e.g., <see cref="GeminiSharp.Client.GeminiClient"/> 
        /// uses "gemini-2.0-flash" as a fallback if this is empty).
        /// </remarks>
        public string DefaultModel { get; set; } = "gemini-1.5-flash";

        /// <summary>
        /// Gets or sets the maximum number of retry attempts for transient API failures.
        /// </summary>
        /// <remarks>
        /// Defaults to 3. Must be non-negative. Used by the retry logic to determine how many times to retry 
        /// transient errors (e.g., 429 Too Many Requests, 503 Service Unavailable).
        /// </remarks>
        public int MaxRetries { get; set; } = 3;

        /// <summary>
        /// Gets or sets the initial delay in seconds between retry attempts, used as the base for exponential backoff.
        /// </summary>
        /// <remarks>
        /// Defaults to 2. Must be positive. The actual delay increases exponentially with each retry attempt 
        /// (e.g., 2^0, 2^1, 2^2 seconds).
        /// </remarks>
        public int BackoffSeconds { get; set; } = 2;
    }
}