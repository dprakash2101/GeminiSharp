using System;
using System.Net;
using GeminiSharp.Models.Error;

namespace GeminiSharp.API
{
    /// <summary>
    /// Represents an exception that occurs during a Gemini API request.
    /// </summary>
    public class GeminiApiException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code of the API response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the detailed error response from the API, if available.
        /// </summary>
        public ApiErrorResponse? ErrorResponse { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeminiApiException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="statusCode">The HTTP status code of the API response.</param>
        /// <param name="errorResponse">The detailed error response from the API.</param>
        public GeminiApiException(string message, HttpStatusCode statusCode, ApiErrorResponse? errorResponse = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorResponse = errorResponse;
        }
    }
}