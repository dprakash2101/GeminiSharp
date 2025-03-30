using System.Net.Http;
using GeminiSharp.API;
using GeminiSharp.API.Interface;
using GeminiSharp.Client.Interface;
using GeminiSharp.Models.Options;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiClient"/> class with configuration options.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for API requests, typically provided by <see cref="IHttpClientFactory"/>.</param>
        /// <param name="options">The configuration options including API key, base URL, default model, and retry settings.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"/> or <paramref name="options"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when required properties in <paramref name="options"/> are not set.</exception>
        public GeminiClient(HttpClient httpClient, GeminiClientOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            _apiClient = new GeminiApiClient(httpClient, options);
            _defaultModel = !string.IsNullOrWhiteSpace(options.DefaultModel)
                ? options.DefaultModel
                : throw new InvalidOperationException("DefaultModel must be set in the configuration.");
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
                return await _apiClient.GenerateContentAsync(_defaultModel, request).ConfigureAwait(false);
            }
            catch (GeminiApiException)
            {
                throw; // Rethrow specific API errors for better debugging.
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while generating content.", ex);
            }
        }
    }
}
