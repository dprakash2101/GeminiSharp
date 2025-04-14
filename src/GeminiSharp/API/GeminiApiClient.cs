using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Utilities; // For RetryConfig
using Serilog; // Only Serilog for logging

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
        /// Sends a request to the Gemini API for content generation (text, images, or structured).
        /// </summary>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="request">The request payload.</param>
        /// <param name="endpoint">The API endpoint (ignored, always uses "generateContent").</param>
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
            int attempt = 0;
            Exception? lastException = null;

            while (attempt <= _retryConfig.MaxRetries)
            {
                try
                {
                    // Log request details
                    
                    Log.Information("Attempt {Attempt} of {MaxRetries} sending request to {Url} with body: {RequestContent}",
                        attempt + 1, _retryConfig.MaxRetries + 1, url, requestContent);

                    var response = await _httpClient.PostAsJsonAsync(url, request);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(
                            errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        // Check if status code is retryable
                        if (attempt < _retryConfig.MaxRetries && _retryConfig.RetryStatusCodes.Contains((int)response.StatusCode))
                        {
                            int delayMs = _retryConfig.UseExponentialBackoff
                                ? _retryConfig.InitialDelayMs * (int)Math.Pow(2, attempt)
                                : _retryConfig.InitialDelayMs;

                            Log.Information("Retrying due to transient error (status {StatusCode}) on attempt {Attempt}. Waiting {DelayMs}ms. Error: {ErrorContent}",
                                response.StatusCode, attempt + 1, delayMs, errorContent);

                            await Task.Delay(delayMs);
                            attempt++;
                            continue;
                        }

                        // Non-retryable error or max retries reached
                        Log.Error("API request failed with status code {StatusCode}. Error: {ErrorContent}",
                            response.StatusCode, errorContent);

                        throw new GeminiApiException(
                            errorResponse?.Error?.Message ?? "Unknown error occurred",
                            response.StatusCode,
                            errorResponse
                        );
                    }

                    var responseContent = await response.Content.ReadFromJsonAsync<GenerateContentResponse>()
                        ?? throw new InvalidOperationException("Failed to deserialize response from Gemini API.");

                    // Log success
                    Log.Information("Successfully received response for model {Model} on attempt {Attempt}.",
                        model, attempt + 1);

                    return responseContent;
                }
                catch (HttpRequestException ex) // Catch network-related errors
                {
                    if (attempt < _retryConfig.MaxRetries)
                    {
                        int delayMs = _retryConfig.UseExponentialBackoff
                            ? _retryConfig.InitialDelayMs * (int)Math.Pow(2, attempt)
                            : _retryConfig.InitialDelayMs;

                        Log.Information("Retrying due to transient network error on attempt {Attempt}. Waiting {DelayMs}ms. Error: {ErrorMessage}",
                            attempt + 1, delayMs, ex.Message);

                        await Task.Delay(delayMs);
                        attempt++;
                        lastException = ex;
                        continue;
                    }

                    lastException = ex;
                    break;
                }
                catch (Exception ex)
                {
                    // Log non-retryable exception and rethrow
                    Log.Error(ex, "Non-retryable error while sending request for model {Model} on attempt {Attempt}.",
                        model, attempt + 1);
                    throw;
                }
            }

            // If we get here, retries failed
            Log.Error(lastException, "Failed to send request for model {Model} after {MaxRetries} retries.",
                model, _retryConfig.MaxRetries + 1);
            throw new Exception("Failed after maximum retries.", lastException);
        }
    }
}