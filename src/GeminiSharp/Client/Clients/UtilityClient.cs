using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using GeminiSharp.Models.Embeddings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiSharp.Client.Clients
{
    /// <summary>
    /// Provides utility functionalities like embedding content, counting tokens, and retrieving model information.
    /// </summary>
    public class UtilityClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UtilityClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public UtilityClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates embeddings for a given text input.
        /// </summary>
        /// <param name="model">The Gemini model to use for embeddings (e.g., "embedding-001").</param>
        /// <param name="text">The text to generate embeddings for.</param>
        /// <returns>A <see cref="EmbeddingResponse"/> containing the generated embeddings.</returns>
        /// <exception cref="ArgumentException">Thrown if the text is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<EmbeddingResponse> EmbedContentAsync(string? model, string text)
        {
            model = model ?? "embedding-001"; // Set default model for embeddings
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Log.Error("Text for embedding is empty or null in EmbedContentAsync.");
                throw new ArgumentException("Text cannot be empty", nameof(text));
            }

            var request = new EmbedContentRequest
            {
                Content = new RequestContent
                {
                    Parts = new List<RequestContentPart> { new RequestContentPart { Text = text } }
                }
            };

            try
            {
                Log.Information("Generating embeddings for model {Model} with text: {Text}", model, text);
                var response = await _apiClient.SendRequestAsync<EmbedContentRequest, EmbeddingResponse>(model, request, "embedContent");
                Log.Information("Successfully generated embeddings for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating embeddings for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating embeddings for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating embeddings.", ex);
            }
        }

        /// <summary>
        /// Counts the tokens in a given text input.
        /// </summary>
        /// <param name="model">The Gemini model to use for token counting.</param>
        /// <param name="text">The text to count tokens for.</param>
        /// <returns>A <see cref="CountTokensResponse"/> containing the token count.</returns>
        /// <exception cref="ArgumentException">Thrown if the text is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<CountTokensResponse> CountTokensAsync(string? model, string text)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Log.Error("Text for token counting is empty or null in CountTokensAsync.");
                throw new ArgumentException("Text cannot be empty", nameof(text));
            }

            var request = new CountTokensRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart> { new RequestContentPart { Text = text } }
                    }
                }
            };

            try
            {
                Log.Information("Counting tokens for model {Model} with text: {Text}", model, text);
                var response = await _apiClient.SendRequestAsync<CountTokensRequest, CountTokensResponse>(model, request, "countTokens");
                Log.Information("Successfully counted tokens for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while counting tokens for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while counting tokens for model {Model}.", model);
                throw new Exception("An unexpected error occurred while counting tokens.", ex);
            }
        }

        /// <summary>
        /// Retrieves information about a specific model.
        /// </summary>
        /// <param name="model">The model to get information about (e.g., "gemini-1.5-flash").</param>
        /// <returns>A <see cref="ModelInfo"/> object containing model details.</returns>
        /// <exception cref="ArgumentException">Thrown if the model name is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<ModelInfo> GetModelInfoAsync(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                Log.Error("Model name is empty or null in GetModelInfoAsync.");
                throw new ArgumentException("Model name cannot be empty", nameof(model));
            }

            try
            {
                Log.Information("Retrieving model information for {Model}", model);
                var response = await _apiClient.SendGetRequestAsync<ModelInfo>(model);
                Log.Information("Successfully retrieved model information for {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while retrieving model information for {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while retrieving model information for {Model}.", model);
                throw new Exception("An unexpected error occurred while retrieving model information.", ex);
            }
        }

        /// <summary>
        /// Generates content with function calling.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="tools">A list of tools that the model can use.</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt is empty or null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the tools list is null or empty.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentWithFunctionCallingAsync(string? model, string prompt, List<Tool> tools)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentWithFunctionCallingAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (tools == null || !tools.Any())
            {
                Log.Error("Tools list is null or empty in GenerateContentWithFunctionCallingAsync.");
                throw new ArgumentNullException(nameof(tools));
            }

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
                },
                Tools = tools
            };

            try
            {
                Log.Information("Generating content with function calling for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content with function calling for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content with function calling for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content with function calling for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content with function calling.", ex);
            }
        }
    }
}