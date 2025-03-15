using System.Net;
using GeminiSharp.API;

namespace GeminiSharp.Configuration
{
    /// <summary>
    /// Executes tasks with retry logic for transient failures, using an exponential backoff strategy.
    /// </summary>
    public class RetryExecutor
    {
        private readonly int _maxRetries;
        private readonly int _backoffSeconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryExecutor"/> class with retry settings.
        /// </summary>
        /// <param name="maxRetries">The maximum number of retry attempts. Must be non-negative.</param>
        /// <param name="backoffSeconds">The initial delay in seconds between retries, used as the base for exponential backoff. Must be positive.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxRetries"/> is negative or <paramref name="backoffSeconds"/> is not positive.</exception>
        public RetryExecutor(int maxRetries, int backoffSeconds)
        {
            if (maxRetries < 0)
                throw new ArgumentOutOfRangeException(nameof(maxRetries), "Maximum retries cannot be negative.");
            if (backoffSeconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(backoffSeconds), "Backoff seconds must be positive.");

            _maxRetries = maxRetries;
            _backoffSeconds = backoffSeconds;
        }

        /// <summary>
        /// Executes an asynchronous task with retry logic, retrying on transient failures up to the configured maximum attempts.
        /// </summary>
        /// <typeparam name="T">The type of the result returned by the task.</typeparam>
        /// <param name="apiCall">The asynchronous task to execute, typically an API call.</param>
        /// <returns>A task representing the result of the executed operation.</returns>
        /// <exception cref="GeminiApiException">Thrown when all retry attempts fail due to transient errors or a non-transient error occurs.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiCall"/> is null.</exception>
        public async Task<T> ExecuteWithRetries<T>(Func<Task<T>> apiCall)
        {
            if (apiCall == null)
                throw new ArgumentNullException(nameof(apiCall));

            for (int attempt = 0; attempt <= _maxRetries; attempt++)
            {
                try
                {
                    return await apiCall();
                }
                catch (GeminiApiException ex) when (IsTransient(ex.StatusCode) && attempt < _maxRetries)
                {
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(_backoffSeconds, attempt)));
                }
            }
            throw new GeminiApiException("Max retries exceeded", HttpStatusCode.ServiceUnavailable, null);
        }

        /// <summary>
        /// Determines whether an HTTP status code represents a transient failure that warrants a retry.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to evaluate.</param>
        /// <returns><c>true</c> if the status code is transient (e.g., 429, 503, 504); otherwise, <c>false</c>.</returns>
        private bool IsTransient(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.TooManyRequests ||
                   statusCode == HttpStatusCode.ServiceUnavailable ||
                   statusCode == HttpStatusCode.GatewayTimeout;
        }
    }
}