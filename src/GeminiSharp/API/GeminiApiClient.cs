using System.Net.Http.Json;
using System.Text.Json;
using GeminiSharp.API.Interface;
using GeminiSharp.Configuration;
using GeminiSharp.Models.Error;
using GeminiSharp.Models.Options;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;

namespace GeminiSharp.API
{
    public class GeminiApiClient : IGeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly RetryExecutor _retryExecutor;
        private readonly ILogger _logger;

        public GeminiApiClient(HttpClient httpClient, GeminiClientOptions options, ILogger logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = options.ApiKey ?? throw new ArgumentNullException(nameof(options.ApiKey));
            _baseUrl = options.BaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/";
            _retryExecutor = new RetryExecutor(options.MaxRetries, options.BackoffSeconds);
            _logger = logger ?? Log.Logger;
        }

        public async Task<GenerateContentResponse> GenerateContentAsync(string model, GenerateContentRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));

            return await _retryExecutor.ExecuteWithRetries(async () =>
            {
                string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";
                _logger.Information("Sending request to {Url}", url);

                var response = await _httpClient.PostAsJsonAsync(url, request);
                return await HandleResponse(response);
            });
        }

        public async Task<GenerateContentResponse> GenerateStructuredContentAsync(string model, GeminiStructuredRequest request)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));

            return await _retryExecutor.ExecuteWithRetries(async () =>
            {
                string url = $"{_baseUrl}{model}:generateContent?key={_apiKey}";
                _logger.Information("Sending structured request to {Url}", url);

                var response = await _httpClient.PostAsJsonAsync(url, request);
                return await HandleResponse(response);
            });
        }

        private async Task<GenerateContentResponse> HandleResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error("API error: {StatusCode} - {Content}", response.StatusCode, content);
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                throw new GeminiApiException(errorResponse?.Error?.Message ?? "Unknown error occurred", response.StatusCode, errorResponse);
            }

            _logger.Information("API response received successfully.");
            return JsonSerializer.Deserialize<GenerateContentResponse>(content)
                ?? throw new Exception("Failed to deserialize response from Gemini API.");
        }
    }
}
