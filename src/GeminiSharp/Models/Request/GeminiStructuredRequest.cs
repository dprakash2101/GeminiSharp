﻿namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Represents a request for generating structured content using the Gemini API.
    /// </summary>
    public class GeminiStructuredRequest
    {
        /// <summary>
        /// Gets or sets the contents of the request.
        /// </summary>
        public List<RequestContent> Contents { get; set; } = new();

        /// <summary>
        /// Gets or sets the generation configuration for the structured response.
        /// </summary>
        public GenerationConfig GenerationConfig { get; set; } = new();
    }

    /// <summary>
    /// Defines the configuration for generating structured output.
    /// </summary>
    public class GenerationConfig
    {
        /// <summary>
        /// Gets or sets the MIME type of the response. Defaults to "application/json".
        /// </summary>
        public string? response_mime_type { get; set; } = "application/json";

        /// <summary>
        /// Gets or sets the user-defined JSON schema that defines the structured output format.
        /// </summary>
        public object? response_schema { get; set; }  // User-defined class converted to JSON schema
    }
}
