using System.Collections.Generic;

namespace GeminiSharp.Models.Utilities
{
    /// <summary>
    /// Configuration for retrying API requests.
    /// </summary>
    public class RetryConfig
    {
        /// <summary>
        /// Gets or sets the maximum number of retry attempts (default is 3).
        /// </summary>
        public int MaxRetries { get; set; } = 3;

        /// <summary>
        /// Gets or sets the initial delay between retries in milliseconds (default is 1000ms).
        /// </summary>
        public int InitialDelayMs { get; set; } = 1000;

        /// <summary>
        /// Gets or sets a value indicating whether to use exponential backoff (doubles delay each retry, default is true).
        /// </summary>
        public bool UseExponentialBackoff { get; set; } = true;

        /// <summary>
        /// Gets or sets the HTTP status codes that should trigger a retry (default includes 429, 500, 503).
        /// </summary>
        public HashSet<int> RetryStatusCodes { get; set; } = new HashSet<int> { 429, 500, 503 };
    }
}