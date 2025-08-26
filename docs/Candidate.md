# GeminiSharp.Model.Candidate

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Content** | [**ResponseContent**](ResponseContent.md) |  | [optional] 
**FinishReason** | **string** | Output only. The reason why the model stopped generating tokens | [optional] 
**Index** | **int** | Output only. Index of the candidate in the list of response candidates | [optional] 
**SafetyRatings** | [**Collection&lt;SafetyRating&gt;**](SafetyRating.md) | List of ratings for the safety of a response candidate | [optional] 
**CitationMetadata** | [**CitationMetadata**](CitationMetadata.md) |  | [optional] 
**TokenCount** | **int** | Output only. Token count for this candidate | [optional] 
**GroundingAttributions** | [**Collection&lt;GroundingAttribution&gt;**](GroundingAttribution.md) | Output only. Attribution information for sources that contributed to a grounded answer | [optional] 
**GroundingMetadata** | [**GroundingMetadata**](GroundingMetadata.md) |  | [optional] 
**AvgLogprobs** | **double** | Output only. Average log probability of the candidate | [optional] 
**LogprobsResult** | [**LogprobsResult**](LogprobsResult.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

