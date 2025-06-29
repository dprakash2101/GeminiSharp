using GeminiSharp.API;
using GeminiSharp.Helpers;
using GeminiSharp.Models;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeminiSharp.Client
{
    /// <summary>
    /// A client for document understanding with the Gemini API.
    /// </summary>
    public class GeminiDocumentUnderstandingClient
    {
        private readonly GeminiApiClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiDocumentUnderstandingClient"/> class.
        /// </summary>
        /// <param name="apiClient">The Gemini API client.</param>
        public GeminiDocumentUnderstandingClient(GeminiApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Generates content from a single document and a question.
        /// </summary>
        /// <param name="documentPath">The path to the document file.</param>
        /// <param name="question">The question to ask about the document.</param>
        /// <param name="mimeType">The MIME type of the document. If not provided, it will be inferred from the file extension.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>The generated content as a string.</returns>
        public async Task<GenerateContentResponse> GenerateContentAsync(string documentPath, string question, string? mimeType = null, CancellationToken cancellationToken = default)
        {
            var documentPart = await DocumentHelper.ToRequestContentPart(documentPath, mimeType);

            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = new List<RequestContentPart>
                        {
                            documentPart,
                            new RequestContentPart { Text = question }
                        }
                    }
                }
            };

            var response = await _apiClient.GenerateContentAsync<GenerateContentRequest, GenerateContentResponse>(request, cancellationToken);
            return response;
        }

        /// <summary>
        /// Generates content from multiple documents and a question.
        /// </summary>
        /// <param name="documentPaths">A collection of document file paths.</param>
        /// <param name="question">The question to ask about the documents.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>The generated content as a string.</returns>
        public async Task<GenerateContentResponse> GenerateContentAsync(IEnumerable<string> documentPaths, string question, CancellationToken cancellationToken = default)
        {
            var documentParts = await Task.WhenAll(documentPaths.Select(p => DocumentHelper.ToRequestContentPart(p)));

            if (documentParts == null || documentParts.Any(dp => dp == null))
            {
                throw new InvalidOperationException("One or more document parts could not be created.");
            }

            var parts = documentParts.ToList();
            parts.Add(new RequestContentPart { Text = question });

            var request = new GenerateContentRequest
            {
                Contents = new List<RequestContent>
                {
                    new RequestContent
                    {
                        Parts = parts
                    }
                }
            };

            var response = await _apiClient.GenerateContentAsync<GenerateContentRequest, GenerateContentResponse>(request, cancellationToken);
            return response;
        }
    }
}
