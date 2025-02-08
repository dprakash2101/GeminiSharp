using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Error;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Provides a high-level interface for interacting with the Gemini API.
    /// </summary>
    public class GeminiClient
    {
        private readonly GeminiApiClient _apiClient;

        public GeminiClient(HttpClient httpClient, string apiKey, string? baseUrl = null)
        {
            _apiClient = new GeminiApiClient(httpClient, apiKey, baseUrl);
        }

        /// <summary>
        /// Generates content based on user input and returns the full response.
        /// If an error occurs (for example, an invalid API key or non-existent model),
        /// the API error details are propagated via a GeminiApiException.s
        /// </summary>
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
            catch (GeminiApiException ex)
            {
                // The error details from the API are already captured in the exception (ex).
                // Rethrow it so that the consumer can inspect the ApiErrorResponse.
                throw;
            }
            catch (Exception ex)
            {
                // For any unexpected exceptions, wrap them with a generic message.
                throw new Exception("An unexpected error occurred while generating content.", ex);
            }
        }
    }
}
