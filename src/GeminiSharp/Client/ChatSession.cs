using GeminiSharp.API;
using GeminiSharp.Models.Request;
using GeminiSharp.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiSharp.Client
{
    /// <summary>
    /// Represents a chat session with the Gemini API, managing conversation history.
    /// </summary>
    public class ChatSession
    {
        private readonly string _model;
        private readonly GeminiApiClient _apiClient;
        private readonly List<RequestContent> _history = new List<RequestContent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatSession"/> class.
        /// </summary>
        /// <param name="model">The Gemini model to use for the chat.</param>
        /// <param name="apiClient">The internal API client for sending requests.</param>
        internal ChatSession(string model, GeminiApiClient apiClient)
        {
            _model = model;
            _apiClient = apiClient;
        }

        /// <summary>
        /// Sends a message to the model and appends the user's message and the model's response to the conversation history.
        /// </summary>
        /// <param name="prompt">The user's message.</param>
        /// <returns>The model's response.</returns>
        /// <exception cref="GeminiApiException">Thrown if the API returns an error.</exception>
        /// <exception cref="Exception">Thrown for unexpected errors.</exception>
        public async Task<GenerateContentResponse> SendMessageAsync(string prompt)
        {
            var userContent = new RequestContent { Parts = new List<RequestContentPart> { new RequestContentPart { Text = prompt } } };
            _history.Add(userContent);

            var request = new GenerateContentRequest
            {
                Contents = _history
            };

            var response = await _apiClient.SendRequestAsync<GenerateContentRequest, GenerateContentResponse>(_model, request, "generateContent");

            if (response.Candidates != null && response.Candidates.Any())
            {
                var modelContent = response.Candidates.First().Content;
                if (modelContent != null)
                {
                    _history.Add(new RequestContent { Parts = modelContent.Parts?.Select(p => new RequestContentPart { Text = p.Text }).ToList() });
                }
            }

            return response;
        }
    }