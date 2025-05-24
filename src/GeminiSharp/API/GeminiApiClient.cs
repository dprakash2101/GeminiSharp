using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Utilities; // For RetryConfig
using Polly;
using Polly.Retry;
using Serilog;

namespace GeminiSharp.API
{
    /// <summary>
    /// A reusable client for interacting with the Google Gemini API.
    /// </summary>
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly RetryConfig _retryConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="httpClient">The HTTP client instance (optional, defaults to a new instance).</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional, defaults to Google's endpoint).</param>
        /// <param name="retryConfig">Retry configuration (optional, defaults to 3 retries for 429, 500, 503).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiKey"/> is null or empty.</exception>
        public GeminiApiClient(string apiKey, HttpClient? httpClient = null, string? baseUrl = null, RetryConfig? retryConfig = null)
        {
            _apiKey = string.IsNullOrWhiteSpace(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            _httpClient = httpClient ?? new HttpClient();
            _baseUrl = baseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
            _retryConfig = retryConfig ?? new RetryConfig();
        }

        /// <summary>
        /// Sends a request to the Gemini API for content generation (text, images, or structured output).
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="GenerateContentResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response after retries.</exception>
        /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
        public async Task<GenerateContentResponse> SendRequestAsync<TRequest>(string model, TRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                Log.Error("Model name is empty or null.");
                throw new ArgumentException("Model cannot be empty", nameof(model));
            }

            string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";
            string requestContent = JsonSerializer.Serialize(request);

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => _retryConfig.RetryStatusCodes.Contains((int)r.StatusCode))
                .WaitAndRetryAsync(
                    retryCount: _retryConfig.MaxRetries,
                    sleepDurationProvider: attempt =>
                        TimeSpan.FromMilliseconds(
                            _retryConfig.UseExponentialBackoff
                                ? _retryConfig.InitialDelayMs * Math.Pow(2, attempt - 1)
                                : _retryConfig.InitialDelayMs),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        if (outcome.Exception != null)
                        {
                            Log.Warning(outcome.Exception,
                                "Retry {RetryAttempt} due to exception. Waiting {Delay}ms.",
                                retryAttempt, timespan.TotalMilliseconds);
                        }
                        else if (outcome.Result != null)
                        {
                            Log.Warning("Retry {RetryAttempt} due to status code {StatusCode}. Waiting {Delay}ms.",
                                retryAttempt, (int)outcome.Result.StatusCode, timespan.TotalMilliseconds);
                        }
                    });

            try
            {
                Log.Information("Sending request to {Url} with body: {RequestContent}", url, requestContent);

                var response = await retryPolicy.ExecuteAsync(() =>
                    _httpClient.PostAsJsonAsync(url, request));

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(
                        errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    Log.Error("API request failed after retries. Status code {StatusCode}, Error: {ErrorContent}",
                        response.StatusCode, errorContent);

                    throw new GeminiApiException(
                        errorResponse?.Error?.Message ?? "Unknown error occurred",
                        response.StatusCode,
                        errorResponse
                    );
                }

                var responseContent = await response.Content.ReadFromJsonAsync<GenerateContentResponse>()
                    ?? throw new InvalidOperationException("Failed to deserialize response from Gemini API.");

                Log.Information("Successfully received response for model {Model}.", model);
                return responseContent;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Request to Gemini API failed for model {Model}.", model);
                throw;
            }
        }
    }
}
