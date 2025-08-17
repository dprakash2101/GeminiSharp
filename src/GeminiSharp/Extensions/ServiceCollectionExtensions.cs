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
            configureOptions(options);

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new ArgumentException("API Key must be provided in GeminiSharpOptions.", nameof(configureOptions));
            }

            services.AddHttpClient<GeminiApiClient>(client =>
            {
                client.BaseAddress = new Uri(options.BaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/");
                client.DefaultRequestHeaders.Add("x-goog-api-key", options.ApiKey);
            })
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var retryConfig = options.RetryConfig ?? new RetryConfig();
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => retryConfig.RetryStatusCodes.Contains((int)msg.StatusCode))
                    .WaitAndRetryAsync(retryConfig.MaxRetries, retryAttempt =>
                        TimeSpan.FromMilliseconds(retryConfig.UseExponentialBackoff
                            ? (retryConfig.InitialDelayMs) * Math.Pow(2, retryAttempt - 1)
                            : (retryConfig.InitialDelayMs))
                    );
            });

            services.AddSingleton(options);
            services.AddScoped<IGeminiClient, GeminiClient>();

            return services;
        }
    }
}

