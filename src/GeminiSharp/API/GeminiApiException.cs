using System;
using System.Net;
using GeminiSharp.Models.Error;

namespace GeminiSharp.API
{
    /// <summary>
    /// Represents an exception thrown by the Gemini API.
    /// </summary>
    public class GeminiApiException : Exception
    {
        /// <summary>
        /// The HTTP status code of the error response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The deserialized error response from the API.
        /// </summary>
        public ApiErrorResponse? ErrorResponse { get; }

        /// <summary>
        /// The raw error content from the API response.
        /// </summary>
        public string? RawErrorContent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiException"/> class with a deserialized error response.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorResponse">The deserialized error response.</param>
        public GeminiApiException(string message, HttpStatusCode statusCode, ApiErrorResponse? errorResponse)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorResponse = errorResponse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiException"/> class with raw error content.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="rawErrorContent">The raw error content.</param>
        public GeminiApiException(string message, HttpStatusCode statusCode, string? rawErrorContent = null)
            : base(message)
        {
            StatusCode = statusCode;
            RawErrorContent = rawErrorContent;
        }
    }
}
