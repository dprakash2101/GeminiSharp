namespace GeminiSharp.Models.Utilities
{
    /// <summary>
    /// Configuration for retrying API requests.
    /// </summary>
    public class RetryConfig
    {
        /// <summary>
        /// Number of retry attempts (default is 3).
        /// </summary>
        public int MaxRetries { get; set; } = 3;

        /// <summary>
        /// Initial delay between retries in milliseconds (default is 1000ms).
        /// </summary>
        public int InitialDelayMs { get; set; } = 1000;

        /// <summary>
        /// Whether to use exponential backoff (doubles delay each retry, default is false).
        /// </summary>
        public bool UseExponentialBackoff { get; set; } = true;

        /// <summary>
        /// HTTP status codes to retry (default includes 429, 500, 503).
        /// </summary>
        public HashSet<int> RetryStatusCodes { get; set; } = new HashSet<int> { 429, 500, 503 };
    }
}
