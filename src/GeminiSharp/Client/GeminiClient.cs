using System.Net.Http;
using GeminiSharp.API;
using GeminiSharp.Models.Options;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides a high-level interface for interacting with the Gemini API to generate text content.
    /// </summary>
    /// <remarks>
    /// This class is designed to be used with Dependency Injection (DI) in ASP.NET Core, 
    /// where configuration is provided via <see cref="GeminiClientOptions"/> from appsettings.json.
    /// </remarks>
    public class GeminiClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Gets the default model to use when generating content if not explicitly specified.
        /// </summary>
        /// <remarks>
        /// This value is sourced from the <see cref="GeminiClientOptions.DefaultModel"/> 
        /// configured in appsettings.json.
        /// </remarks>
        public string DefaultModel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class with configuration options.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests, typically provided by <see cref="IHttpClientFactory"/>.</param>
        /// <param name="options">The configuration options including API key, base URL, default model, and retry settings.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="options"/> is null, or when required options properties are null.</exception>
        public GeminiClient(HttpClient httpClient, GeminiClientOptions options)
        {
            _apiClient = new GeminiApiClient(httpClient, options ?? throw new ArgumentNullException(nameof(options)));
            DefaultModel = options.DefaultModel;
        }

        /// <summary>
        /// Generates content asynchronously based on the specified model and user prompt.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The user prompt to generate content from.</param>
        /// <returns>A task representing the generated content response, or <c>null</c> if an error occurs and is not rethrown.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is null or whitespace.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response (e.g., invalid API key or non-existent model).</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during content generation.</exception>
        public async Task<GenerateContentResponse?> GenerateContentAsync(string model, string prompt)
        {
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
                return await _apiClient.GenerateContentAsync(model, request);
            }
            catch (GeminiApiException)
            {
                // Rethrow API-specific errors for the consumer to handle
                throw;
            }
            catch (Exception ex)
            {
                // Wrap unexpected errors with a generic message
                throw new Exception("An unexpected error occurred while generating content.", ex);
            }
        }

        /// <summary>
        /// Generates content asynchronously using the default model and user prompt.
        /// </summary>
        /// <param name="prompt">The user prompt to generate content from.</param>
        /// <returns>A task representing the generated content response, or <c>null</c> if an error occurs and is not rethrown.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <see cref="DefaultModel"/> is not set in the configuration.</exception>
        /// <exception cref="GeminiApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during content generation.</exception>
        public Task<GenerateContentResponse?> GenerateContentAsync(string prompt)
        {
            if (string.IsNullOrEmpty(DefaultModel))
                throw new InvalidOperationException("DefaultModel must be set in the configuration to use this overload.");
            return GenerateContentAsync(DefaultModel, prompt);
        }
    }
}