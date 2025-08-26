# GeminiSharp.Model.TunedModel

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | Output only. The tuned model name | [optional] 
**DisplayName** | **string** | Optional. The name to display for this model in user interfaces | [optional] 
**Description** | **string** | Optional. A short description of this model | [optional] 
**State** | **string** | Output only. The state of the tuned model | [optional] 
**CreateTime** | **DateTime** | Output only. The timestamp when this model was created | [optional] 
**UpdateTime** | **DateTime** | Output only. The timestamp when this model was updated | [optional] 
**TunedModelSource** | [**TunedModelSource**](TunedModelSource.md) |  | [optional] 
**BaseModel** | **string** | Immutable. The name of the base Model this TunedModel is based on | [optional] 
**TuningTask** | [**TuningTask**](TuningTask.md) |  | [optional] 
**Temperature** | **float** | Optional. Controls the randomness of the output | [optional] 
**TopP** | **float** | Optional. For Nucleus sampling | [optional] 
**TopK** | **int** | Optional. For Top-k sampling | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

