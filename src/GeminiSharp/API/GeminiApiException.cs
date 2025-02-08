using System;
using System.Net;
using GeminiSharp.Models.Error;

namespace GeminiSharp.API
{
    public class GeminiApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public ApiErrorResponse? ErrorResponse { get; }

        public GeminiApiException(string message, HttpStatusCode statusCode, ApiErrorResponse? errorResponse = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorResponse = errorResponse;
        }
    }
}
