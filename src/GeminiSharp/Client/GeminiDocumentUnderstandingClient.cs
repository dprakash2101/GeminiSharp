using GeminiSharp.API;
using GeminiSharp.Helpers;
using GeminiSharp.Models;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
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
        /// Generates content from a document and a question.
        /// </summary>
        /// <param name="documentPath">The path to the document file.</param>
        /// <param name="question">The question to ask about the document.</param>
        /// <param name="mimeType">The MIME type of the document. If not provided, it will be inferred from the file extension.</param>
        /// <param name="cancellationToken"> The cancellation token to cancel the operation. </param>
        /// <returns>The generated content.</returns>
        public async Task<string> GenerateContentAsync(string documentPath, string question, string? mimeType = null, CancellationToken cancellationToken = default)
        {
            var documentPart = await DocumentHelper.ToRequestContentPart(documentPath, mimeType);
            var request = new GenerateContentRequest
            {
                Contents = new[]
                {
                    new RequestContent
                    {
                        Parts = new[]
                        {
                            documentPart,
                            new RequestContentPart { Text = question }
                        }
                    }
                }
            };

            var response = await _apiClient.GenerateContentAsync<GenerateContentRequest, GenerateContentResponse>(request, cancellationToken);
            return response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text ?? string.Empty;
        }

        /// <summary>
        /// Generates content from multiple documents and a question.
        /// </summary>
        /// <param name="documentPaths">The paths to the document files.</param>
        /// <param name="question">The question to ask about the documents.</param>
        /// <param name="cancellationToken"> The cancellation token to cancel the operation. </param>
        /// <returns>The generated content.</returns>
        public async Task<string> GenerateContentAsync(IEnumerable<string> documentPaths, string question, CancellationToken cancellationToken = default)
        {
            var documentParts = await Task.WhenAll(documentPaths.Select(p => DocumentHelper.ToRequestContentPart(p)));
            if (documentParts == null)
            {
                throw new GeminiException("Document parts is not null here");
            }
            var request = new GenerateContentRequest
            {
                Contents = new[]
                {
                    new RequestContent
                    {
                        Parts = documentParts.Concat(new[] { new RequestContentPart { Text = question } }).ToArray()
                    }
                }
            };

            var response = await _apiClient.GenerateContentAsync<GenerateContentRequest, GenerateContentResponse>(request, cancellationToken);
            return response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text ?? string.Empty;
        }
    }
}
