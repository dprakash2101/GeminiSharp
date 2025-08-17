using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GeminiSharp.Models.Error
{
    /// <summary>
    /// Represents the top-level error response structure from the Gemini API.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Gets or sets the detailed error information.
        /// </summary>
        [JsonPropertyName("error")]
        public ApiError? Error { get; set; }
    }

    /// <summary>
    /// Represents detailed error information from the Gemini API.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the error.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets a human-readable error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the status of the error (e.g., "INVALID_ARGUMENT", "UNAUTHENTICATED").
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets a list of additional error details.
        /// </summary>
        [JsonPropertyName("details")]
        public List<ApiErrorDetail>? Details { get; set; }
    }

    /// <summary>
    /// Represents a single detail within an API error response.
    /// </summary>
    public class ApiErrorDetail
    {
        /// <summary>
        /// Gets or sets the type of the error detail.
        /// </summary>
        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the reason for the error detail.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the domain of the error detail.
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of metadata associated with the error detail.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, string>? Metadata { get; set; }
    }
}