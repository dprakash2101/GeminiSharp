using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeminiSharp.Models.Options
{
    public class GeminiClientOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models/";
        public string DefaultModel { get; set; } = "gemini-1.5-flash";
        public int MaxRetries { get; set; } = 3; // For retry logic
        public int BackoffSeconds { get; set; } = 2; // For retry logic
    }
}
