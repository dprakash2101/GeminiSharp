# GeminiSharp.Model.GenerationConfig

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**StopSequences** | **Collection&lt;string&gt;** | The set of character sequences that will stop output generation | [optional] 
**ResponseMimeType** | **string** | Output format of the generated candidate text | [optional] 
**ResponseSchema** | **Object** | Output schema of the generated candidate text when response_mime_type is application/json | [optional] 
**CandidateCount** | **int** | Number of generated responses to return | [optional] 
**MaxOutputTokens** | **int** | The maximum number of tokens to include in a response candidate | [optional] 
**Temperature** | **float** | Controls the randomness of the output | [optional] 
**TopP** | **float** | The maximum cumulative probability of tokens to consider when sampling | [optional] 
**TopK** | **int** | The maximum number of tokens to consider when sampling | [optional] 
**PresencePenalty** | **float** | Presence penalty applied to the next token&#39;s logprobs | [optional] 
**FrequencyPenalty** | **float** | Frequency penalty applied to the next token&#39;s logprobs | [optional] 
**ResponseLogprobs** | **bool** | If true, export the logprobs results in response | [optional] 
**Logprobs** | **int** | Only valid if responseLogprobs is True | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

