using Newtonsoft.Json;
using System.Collections.Generic;

namespace GeminiSharp.Models.Request
{
    /// <summary>
    /// Generation configuration for the model
    /// </summary>
    public class GenerationConfigs
    {
        /// <summary>
        /// Optional. Controls the randomness of the output.
        /// Values can range from [0.0,1.0], inclusive. A value closer to 1.0 will produce responses that are more varied and creative, while a value closer to 0.0 will typically result in more straightforward responses from the model.
        /// </summary>
        [JsonProperty("temperature", NullValueHandling = NullValueHandling.Ignore)]
        public float? Temperature { get; set; }

        /// <summary>
        /// Optional. The maximum number of tokens to include in a candidate.
        /// If unset, this will default to output_token_limit specified in the model's specification.
        /// </summary>
        [JsonProperty("maxOutputTokens", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxOutputTokens { get; set; }

        /// <summary>
        /// Optional. The maximum number of candidates to return.
        /// This value must be between [1, 8], inclusive. If unset, this will default to 1.
        /// </summary>
        [JsonProperty("candidateCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? CandidateCount { get; set; }

        /// <summary>
        /// Optional. The set of character sequences (up to 5) that will stop output generation.
        /// If specified, the API will stop at the first appearance of a stop sequence. The stop sequence will not be included as part of the response.
        /// </summary>
        [JsonProperty("stopSequences", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> StopSequences { get; set; }

        /// <summary>
        /// Optional. The maximum cumulative probability of tokens to consider when sampling.
        /// The model uses combined Top-k and nucleus sampling.
        /// Tokens are sorted based on their assigned probabilities so that only the most likely tokens are considered. Top-k sampling directly limits the number of tokens to consider, while nucleus sampling limits the number of tokens based on the cumulative probability.
        /// </summary>
        [JsonProperty("topP", NullValueHandling = NullValueHandling.Ignore)]
        public float? TopP { get; set; }

        /// <summary>
        /// Optional. The maximum number of tokens to consider when sampling.
        /// The model uses combined Top-k and nucleus sampling.
        /// Top-k sampling is useful for preventing the model from generating nonsense words.
        /// </summary>
        [JsonProperty("topK", NullValueHandling = NullValueHandling.Ignore)]
        public int? TopK { get; set; }
    }
}
