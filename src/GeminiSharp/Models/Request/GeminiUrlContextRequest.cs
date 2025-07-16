using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeminiSharp.Models.Request
{
    public class GeminiUrlContextRequest
    {
        [JsonPropertyName("contents")]
        public List<RequestContent>? Contents { get; set; }
        [JsonPropertyName("tools")]
        public List<Tools>? tools { get; set; } 
    }
    public class  Tools
    {
        [JsonPropertyName("url_context")]
        public UrlContext UrlContext { get; set; } = new();
    }

    public class UrlContext
    {
        
    }
}
