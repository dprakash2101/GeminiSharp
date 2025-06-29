using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Models.Configuration;
using GeminiSharp.Models.Error;
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
        private readonly ResiliencePipeline<HttpResponseMessage> _resiliencePipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="httpClient">The HTTP client instance.</param>
        /// <param name="baseUrl">The base URL of the Gemini API (optional, defaults to Google's endpoint).</param>
        /// <param name="retryConfiguration">The configuration for retries (optional, defaults to 3 retries with exponential backoff).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="apiKey"/> is null or empty.</exception>
        public GeminiApiClient(string apiKey, HttpClient httpClient, string? baseUrl = null, RetryConfiguration? retryConfiguration = null)
        {
            _apiKey = string.IsNullOrWhiteSpace(apiKey) ? throw new ArgumentNullException(nameof(apiKey)) : apiKey;
            _httpClient = httpClient;
            _baseUrl = baseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
            retryConfiguration ??= new RetryConfiguration();

            _resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
                .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<HttpRequestException>()
                        .HandleResult(response =>
                            response.StatusCode == HttpStatusCode.TooManyRequests ||
                            response.StatusCode >= HttpStatusCode.InternalServerError),
                    Delay = retryConfiguration.Delay,
                    MaxRetryAttempts = retryConfiguration.MaxRetryAttempts,
                    BackoffType = retryConfiguration.BackoffType,
                    UseJitter = retryConfiguration.UseJitter,
                    OnRetry = args =>
                    {
                        Log.Warning("Request failed with {StatusCode}. Retrying in {Delay}. Attempt {AttemptNumber} of {MaxRetryAttempts}",
                            args.Outcome.Result?.StatusCode, args.RetryDelay, args.AttemptNumber, retryConfiguration.MaxRetryAttempts);
                        return default;
                    }
                })
                .Build();
        }

        /// <summary>
        /// Sends a request to the Gemini API and handles retries using a Polly resilience pipeline.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <typeparam name="TResponse">The type of the response payload.</typeparam>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload.</param>
        /// <param name="endpoint">The API endpoint to target.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="TResponse"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the model is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error after retries.</exception>
        /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(string model, TRequest request, string endpoint, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                Log.Error("Model name is empty or null.");
                throw new ArgumentException("Model cannot be empty", nameof(model));
            }

            var url = $"{_baseUrl}{model}:{endpoint}?key={_apiKey}";
            var requestContent = JsonSerializer.Serialize(request);

            Log.Information("Sending request to {Url} with body: {RequestContent}", url, requestContent);

            var response = await _resiliencePipeline.ExecuteAsync(async token =>
                await _httpClient.PostAsJsonAsync(url, request, token),
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException("Failed to deserialize response from Gemini API.");
                
                Log.Information("Successfully received response for model {Model}.", model);
                return responseContent;
            }

            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(errorContent))
            {
                Log.Error("API request failed with status code {StatusCode} and an empty error response.", response.StatusCode);
                throw new GeminiApiException($"API request failed with status code {response.StatusCode}.", response.StatusCode);
            }

            try
            {
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(
                    errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                Log.Error("API request failed with status code {StatusCode}. Error: {ErrorContent}", response.StatusCode, errorContent);

                throw new GeminiApiException(
                    errorResponse?.Error?.Message ?? "Unknown error occurred",
                    response.StatusCode,
                    errorResponse
                );
            }
            catch (JsonException ex)
            {
                Log.Error(ex, "Failed to deserialize error response from Gemini API. Response content: {ErrorContent}", errorContent);
                throw new GeminiApiException("Failed to deserialize error response from Gemini API.", response.StatusCode, errorContent);
            }
        }
    }
}