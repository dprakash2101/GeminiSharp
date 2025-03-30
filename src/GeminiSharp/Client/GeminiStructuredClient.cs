using System.Net.Http;
using GeminiSharp.API;
using GeminiSharp.Client.Interface;
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
    public class GeminiStructuredClient: IGeminiStructuredClient
    {
        private readonly GeminiApiClient _apiClient;
        private readonly string _defaultModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiStructuredClient"/> class with configuration options.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests, typically provided by <see cref="IHttpClientFactory"/>.</param>
        /// <param name="options">The configuration options including API key, base URL, default model, and retry settings.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="options"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when required properties in <paramref name="options"/> are not set.</exception>
        public GeminiStructuredClient(HttpClient httpClient, GeminiClientOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            _apiClient = new GeminiApiClient(httpClient, options);
            _defaultModel = !string.IsNullOrWhiteSpace(options.DefaultModel)
                ? options.DefaultModel
                : throw new InvalidOperationException("DefaultModel must be set in the configuration.");
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
                return await _apiClient.GenerateStructuredContentAsync(_defaultModel, request).ConfigureAwait(false);
            }
            catch (GeminiApiException)
            {
                throw; // Let API errors bubble up for proper handling.
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while generating structured content.", ex);
            }
        }
    }
}

