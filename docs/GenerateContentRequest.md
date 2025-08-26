# GeminiSharp.Model.GenerateContentRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Contents** | [**Collection&lt;RequestContent&gt;**](RequestContent.md) | Required. The content of the current conversation with the model | [optional] 
**Tools** | [**Collection&lt;Tool&gt;**](Tool.md) | Optional. A list of Tools the Model may use to generate the next response | [optional] 
**ToolConfig** | [**ToolConfig**](ToolConfig.md) |  | [optional] 
**SafetySettings** | [**Collection&lt;SafetySetting&gt;**](SafetySetting.md) | Optional. A list of unique SafetySetting instances for blocking unsafe content | [optional] 
**SystemInstruction** | [**RequestContent**](RequestContent.md) |  | [optional] 
**GenerationConfig** | [**GenerationConfig**](GenerationConfig.md) |  | [optional] 
**CachedContent** | **string** | Optional. The name of the cached content to use as context | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

