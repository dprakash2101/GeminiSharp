using GeminiSharp.Client;
using GeminiSharp.API;
using GeminiSharp.Models.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace GeminiSharp.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IServiceCollection"/> to add Gemini API client services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Gemini API client and its dependencies to the specified <see cref="IServiceCollection"/>.
        /// Configures HttpClient with Polly for transient fault handling (retries).
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="apiKey">The API key for authentication with the Gemini API.</param>
        /// <param name="configureRetry">Optional. An action to configure the retry policy settings.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddGeminiClient(this IServiceCollection services, string apiKey, Action<RetryConfig>? configureRetry = null)
        {
            var retryConfig = new RetryConfig();
            configureRetry?.Invoke(retryConfig);

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => retryConfig.RetryStatusCodes.Contains((int)msg.StatusCode))
                .WaitAndRetryAsync(retryConfig.MaxRetries, retryAttempt =>
                    TimeSpan.FromMilliseconds(retryConfig.UseExponentialBackoff
                        ? retryConfig.InitialDelayMs * Math.Pow(2, retryAttempt - 1)
                        : retryConfig.InitialDelayMs)
                );

            services.AddHttpClient<GeminiApiClient>()
                    .AddPolicyHandler(retryPolicy);

            services.AddSingleton<GeminiApiClient>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                return new GeminiApiClient(apiKey, httpClientFactory, retryConfig: retryConfig);
            });

            services.AddSingleton<IGeminiClient, GeminiClient>();

            return services;
        }
    }
}