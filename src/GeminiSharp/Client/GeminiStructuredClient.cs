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
    /// Provides an interface for generating structured output using the Gemini API.
    /// </summary>
    public class GeminiStructuredClient : IGeminiStructuredClient
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
        public GeminiStructuredClient(IGeminiApiClient apiClient, GeminiClientOptions options, ILogger logger)
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
        /// Generates structured content asynchronously using the configured model, user prompt, and JSON schema.
        /// </summary>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <returns>A task representing the structured content response.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="prompt"/> is null or empty.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        public async Task<GenerateContentResponse?> GenerateStructuredContentAsync(string prompt, object jsonSchema)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));

            if (jsonSchema == null)
                throw new ArgumentNullException(nameof(jsonSchema), "JSON schema cannot be null.");

            var request = new GeminiStructuredRequest
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
                },
                GenerationConfig = new GenerationConfig
                {
                    response_mime_type = "application/json",
                    response_schema = jsonSchema
                }
            };

            try
            {
                _logger.Information("Sending structured content request to Gemini API with model {Model}", _defaultModel);
                var response = await _apiClient.GenerateStructuredContentAsync(_defaultModel, request).ConfigureAwait(false);
                _logger.Information("Received structured content response from Gemini API");
                return response;
            }
            catch (GeminiApiException ex)
            {
                _logger.Error(ex, "Gemini API returned an error");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An unexpected error occurred while generating structured content.");
                throw new InvalidOperationException("An unexpected error occurred while generating structured content.", ex);
            }
        }
    }
}
