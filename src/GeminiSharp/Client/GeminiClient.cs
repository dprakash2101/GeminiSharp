using System.Net.Http;
using GeminiSharp.API;
using GeminiSharp.API.Interface;
using GeminiSharp.Client.Interface;
using GeminiSharp.Models.Options;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides a high-level interface for interacting with the Gemini API to generate text content.
    /// </summary>
    /// <remarks>
    /// This class is designed for use with Dependency Injection (DI) in ASP.NET Core, 
    /// where configuration is provided via <see cref="GeminiClientOptions"/> from appsettings.json.
    /// </remarks>
    public class GeminiClient : IGeminiClient
    {
        private readonly IGeminiApiClient _apiClient;
        private readonly string _defaultModel;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class with configuration options.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance injected via DI.</param>
        /// <param name="options">The configuration options including API key, base URL, and default model.</param>
        /// <param name="logger">The Serilog logger instance.</param>
        public GeminiClient(IGeminiApiClient apiClient, GeminiClientOptions options, ILogger logger)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (options is null)
                throw new ArgumentNullException(nameof(options));

            _defaultModel = !string.IsNullOrWhiteSpace(options.DefaultModel)
                ? options.DefaultModel
                : throw new InvalidOperationException("DefaultModel must be set in the configuration.");

            _logger.Information("GeminiClient initialized with default model: {DefaultModel}", _defaultModel);
        }

        /// <summary>
        /// Generates content asynchronously based on the specified user prompt using the default model.
        /// </summary>
        /// <param name="prompt">The user prompt for content generation.</param>
        /// <returns>A task representing the generated content response.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="prompt"/> is null or empty.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        public async Task<GenerateContentResponse?> GenerateContentAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));

            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart>
                        {
                            new RequestContentPart { Text = prompt }
                        }
                    }
                }
            };

            try
            {
                _logger.Information("Sending content generation request with model: {Model}", _defaultModel);
                _logger.Debug("Request Payload: {@Request}", request);
                var response = await _apiClient.GenerateContentAsync(_defaultModel, request).ConfigureAwait(false);
                _logger.Information("Received content generation response.");
                _logger.Debug("Response Payload: {@Response}", response);
                return response;
            }
            catch (GeminiApiException ex)
            {
                _logger.Error(ex, "Gemini API returned an error");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An unexpected error occurred while generating content.");
                throw new InvalidOperationException("An unexpected error occurred while generating content.", ex);
            }
        }
    }
}
