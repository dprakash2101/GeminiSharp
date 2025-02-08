using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeminiSharp.Models.Error
{
    public class ApiErrorResponse
    {
        [JsonPropertyName("error")]
        public ApiError? Error { get; set; }
    }

    public class ApiError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("details")]
        public List<ApiErrorDetail>? Details { get; set; }
    }

    public class ApiErrorDetail
    {
        [JsonPropertyName("@type")]
        public string? Type { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string>? Metadata { get; set; }
    }
}