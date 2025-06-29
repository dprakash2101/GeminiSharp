using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Models.Configuration;
using GeminiSharp.Models.Error;
using Serilog;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Net.Http;

namespace GeminiSharp.API
{
    /// <summary>
    /// Defines the contract for a client that interacts with the Google Gemini API.
    /// </summary>
    public interface IGeminiApiClient
    {
        /// <summary>
        /// Sends a request to the Gemini API to generate content.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <typeparam name="TResponse">The type of the expected response.</typeparam>
        /// <param name="request">The request payload.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>The response from the API.</returns>
        Task<TResponse> GenerateContentAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : class;
    }

    /// <summary>
    /// A reusable client for interacting with the Google Gemini API.
    /// </summary>
    public class GeminiApiClient : IGeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;
        private readonly ResiliencePipeline<HttpResponseMessage> _resiliencePipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance.</param>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="retryConfiguration">The configuration for retries (optional, defaults to 3 retries with exponential backoff).</param>
        public GeminiApiClient(HttpClient httpClient, string model, RetryConfiguration? retryConfiguration = null)
        {
            _httpClient = httpClient;
            _model = model;
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
        /// <param name="request">The request payload.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>A task representing the asynchronous operation, returning a <see cref="TResponse"/>.</returns>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error after retries.</exception>
        /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
        public async Task<TResponse> GenerateContentAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
             where TRequest : class
        {
            var url = $"{_model}:generateContent";
            var requestContent = JsonSerializer.Serialize(request);

            Log.Information("Sending request to {Url} with body: {RequestContent}", url, requestContent);

            var response = await _resiliencePipeline.ExecuteAsync(async token =>
                await _httpClient.PostAsJsonAsync(url, request, token),
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException("Failed to deserialize response from Gemini API.");

                Log.Information("Successfully received response for model {Model}.", _model);
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