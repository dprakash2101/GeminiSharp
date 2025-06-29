using Polly;
using Polly.Retry;
using System;

namespace GeminiSharp.Models.Configuration
{
    /// <summary>
    /// Configuration for retry mechanism
    /// </summary>
    public class RetryConfiguration
    {
        /// <summary>
        /// Max retry attempts
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 3;

        /// <summary>
        /// Delay between retries
        /// </summary>
        public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Type of backoff
        /// </summary>
        public DelayBackoffType BackoffType { get; set; } = DelayBackoffType.Exponential;

        /// <summary>
        /// Whether to use jitter
        /// </summary>
        public bool UseJitter { get; set; } = true;
    }
}
