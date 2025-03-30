using System.Net.Http;
using GeminiSharp.API;
using GeminiSharp.Models.Options;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides an interface for generating structured output using the Gemini API.
    /// </summary>
    /// <remarks>
    /// This class is designed to be used with Dependency Injection (DI) in ASP.NET Core, 
    /// where configuration is provided via <see cref="GeminiClientOptions"/> from appsettings.json.
    /// </remarks>
    public class GeminiStructuredClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Gets the default model to use when generating structured content if not explicitly specified.
        /// </summary>
        /// <remarks>
        /// This value is sourced from <see cref="GeminiClientOptions.DefaultModel"/> in the configuration.
        /// Defaults to "gemini-1.5-flash" if not specified in appsettings.json.
        /// </remarks>
        public string DefaultModel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiStructuredClient"/> class with configuration options.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests, typically provided by <see cref="IHttpClientFactory"/>.</param>
        /// <param name="options">The configuration options including API key, base URL, default model, and retry settings.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="options"/> is null, or when required options properties are null.</exception>
        public GeminiStructuredClient(HttpClient httpClient, GeminiClientOptions options)
        {
            _apiClient = new GeminiApiClient(httpClient, options ?? throw new ArgumentNullException(nameof(options)));
            DefaultModel = options.DefaultModel;
        }

        /// <summary>
        /// Generates structured content asynchronously based on the specified model, user prompt, and JSON schema.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <returns>A task representing the structured content response, or <c>null</c> if an error occurs and is not rethrown.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is null or whitespace.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during structured content generation.</exception>
        public async Task<GenerateContentResponse?> GenerateStructuredContentAsync(string model, string prompt, object jsonSchema)
        {
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
                    response_mime_type = "application/json", // Fixed to follow .NET naming conventions
                    response_schema = jsonSchema           // Fixed to follow .NET naming conventions
                }
            };

            try
            {
                return await _apiClient.GenerateStructuredContentAsync(model, request);
            }
            catch (GeminiApiException)
            {
                // Rethrow API-specific errors for the consumer to handle
                throw;
            }
            catch (Exception ex)
            {
                // Wrap unexpected errors with a generic message
                throw new Exception("An unexpected error occurred while generating structured content.", ex);
            }
        }

        /// <summary>
        /// Generates structured content asynchronously using the default model, user prompt, and JSON schema.
        /// </summary>
        /// <param name="prompt">The user prompt that instructs the model.</param>
        /// <param name="jsonSchema">The JSON schema defining the structured output format.</param>
        /// <returns>A task representing the structured content response, or <c>null</c> if an error occurs and is not rethrown.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <see cref="DefaultModel"/> is not set in the configuration.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during structured content generation.</exception>
        public Task<GenerateContentResponse?> GenerateStructuredContentAsync(string prompt, object jsonSchema)
        {
            if (string.IsNullOrEmpty(DefaultModel))
                throw new InvalidOperationException("DefaultModel must be set in the configuration to use this overload.");
            return GenerateStructuredContentAsync(DefaultModel, prompt, jsonSchema);
        }
    }
}