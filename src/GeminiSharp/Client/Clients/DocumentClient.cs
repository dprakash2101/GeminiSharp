using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeminiSharp.Client.Clients
{
    /// <summary>
    /// Provides functionality for generating content from documents using the Gemini API.
    /// </summary>
    public class DocumentClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client instance.</param>
        public DocumentClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        /// <summary>
        /// Generates content from a text prompt and a document.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="documentData">The document data as a base64 string.</param>
        /// <param name="mimeType">The MIME type of the document (e.g., "application/pdf").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, documentData, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromDocumentAsync(string? model, string prompt, string documentData, string mimeType)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromDocumentAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(documentData))
            {
                Log.Error("Document data is empty or null in GenerateContentFromDocumentAsync.");
                throw new ArgumentException("Document data cannot be empty", nameof(documentData));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromDocumentAsync.");
                throw new ArgumentException("MIME type cannot be empty", nameof(mimeType));
            }

            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart>
                        {
                            new RequestContentPart { Text = prompt },
                            new RequestContentPart
                            {
                                InlineData = new InlineData
                                {
                                    MimeType = mimeType,
                                    Data = documentData
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from document for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from document for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from document for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from document for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from document.", ex);
            }
        }

        /// <summary>
        /// Generates content from a text prompt and a document URI.
        /// </summary>
        /// <param name="model">The Gemini model to use (e.g., "gemini-1.5-flash").</param>
        /// <param name="prompt">The text prompt for the model.</param>
        /// <param name="documentUri">The URI of the document.</param>
        /// <param name="mimeType">The MIME type of the document (e.g., "application/pdf").</param>
        /// <returns>A <see cref="GenerateContentResponse"/> containing the generated content.</returns>
        /// <exception cref="ArgumentException">Thrown if the prompt, documentUri, or mimeType is empty or null.</exception>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> GenerateContentFromDocumentUriAsync(string? model, string prompt, string documentUri, string mimeType)
        {
            model = model ?? "gemini-2.5-flash"; // Set default model
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                Log.Error("Prompt is empty or null in GenerateContentFromDocumentUriAsync.");
                throw new ArgumentException("Prompt cannot be empty", nameof(prompt));
            }
            if (string.IsNullOrWhiteSpace(documentUri))
            {
                Log.Error("Document URI is empty or null in GenerateContentFromDocumentUriAsync.");
                throw new ArgumentException("Document URI cannot be empty", nameof(documentUri));
            }
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                Log.Error("MIME type is empty or null in GenerateContentFromDocumentUriAsync.");
                throw new ArgumentException("MIME type cannot be empty", nameof(mimeType));
            }

            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart>
                        {
                            new RequestContentPart { Text = prompt },
                            new RequestContentPart
                            {
                                FileData = new FileData
                                {
                                    MimeType = mimeType,
                                    FileUri = documentUri
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                Log.Information("Generating content from document URI for model {Model} with prompt: {Prompt}", model, prompt);
                var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(model, request, "generateContent");
                Log.Information("Successfully generated content from document URI for model {Model}.", model);
                return response;
            }
            catch (GeminiApiException ex)
            {
                Log.Error(ex, "API error while generating content from document URI for model {Model}.", model);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error while generating content from document URI for model {Model}.", model);
                throw new Exception("An unexpected error occurred while generating content from document URI.", ex);
            }
        }
    }
}