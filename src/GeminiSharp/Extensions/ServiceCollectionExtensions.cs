using GeminiSharp.API;
using GeminiSharp.Client;
using GeminiSharp.Models.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace GeminiSharp.Extensions
{
    /// <summary>
    /// Provides extension methods for IServiceCollection to configure and register Gemini clients.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds and configures Gemini clients to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configureOptions">An action to configure the <see cref="GeminiClientOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <exception cref="ArgumentException">Thrown if the API key is not configured.</exception>
        public static IServiceCollection AddGeminiClient(this IServiceCollection services, Action<GeminiClientOptions> configureOptions)
        {
            var options = new GeminiClientOptions();
            configureOptions(options);

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new ArgumentException("ApiKey must be configured.", nameof(options.ApiKey));
            }

            // Configure the underlying HttpClient
            services.AddHttpClient(nameof(GeminiApiClient), httpClient =>
            {
                httpClient.BaseAddress = new Uri(options.BaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/");
                httpClient.DefaultRequestHeaders.Add("x-goog-api-key", options.ApiKey);
            });

            // Register the core API client
            services.AddSingleton<IGeminiApiClient, GeminiApiClient>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(nameof(GeminiApiClient));
                return new GeminiApiClient(httpClient, options.Model, options.RetryConfiguration);
            });

            // Register the specialized clients
            services.AddSingleton<GeminiClient>();
            services.AddSingleton<GeminiImageGenerationClient>();
            services.AddSingleton<GeminiStructuredClient>();
            services.AddSingleton<GeminiDocumentUnderstandingClient>();

            return services;
        }
    }

    /// <summary>
    /// Represents the configuration options for the Gemini clients.
    /// </summary>
    public class GeminiClientOptions
    {
        /// <summary>
        /// The API key for accessing the Gemini API. This is required.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The specific model to be used by the client (e.g., "gemini-pro").
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The base URL for the Gemini API. If not set, a default will be used.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// The configuration for retry logic in case of transient failures.
        /// </summary>
        public RetryConfiguration RetryConfiguration { get; set; }
    }
}
