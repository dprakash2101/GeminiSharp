using GeminiSharp.API;
using GeminiSharp.Client;
using GeminiSharp.Models.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace GeminiSharp.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IServiceCollection"/> to add Gemini API client services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the GeminiSharp client and its dependencies to the specified <see cref="IServiceCollection"/>.
        /// Configures HttpClient with Polly for transient fault handling (retries).
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configureOptions">An action to configure the <see cref="GeminiSharpOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddGeminiSharp(this IServiceCollection services, Action<GeminiSharpOptions> configureOptions)
        {
            var options = new GeminiSharpOptions();
            configureOptions.Invoke(options);

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new ArgumentException("API Key must be provided in GeminiSharpOptions.", nameof(configureOptions));
            }

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => options.RetryConfig?.RetryStatusCodes.Contains((int)msg.StatusCode) ?? false)
                .WaitAndRetryAsync(options.RetryConfig?.MaxRetries ?? 3, retryAttempt =>
                    TimeSpan.FromMilliseconds(options.RetryConfig?.UseExponentialBackoff ?? true
                        ? (options.RetryConfig?.InitialDelayMs ?? 1000) * Math.Pow(2, retryAttempt - 1)
                        : (options.RetryConfig?.InitialDelayMs ?? 1000))
                );

            services.AddHttpClient<GeminiApiClient>(client =>
            {
                client.DefaultRequestHeaders.Add("x-goog-api-key", options.ApiKey);
            });

            services.AddScoped<GeminiApiClient>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                return new GeminiApiClient(options.ApiKey, httpClientFactory, options.BaseUrl, options.RetryConfig);
            });

            services.AddScoped<IGeminiClient, GeminiClient>();

            return services;
        }
    }
}

