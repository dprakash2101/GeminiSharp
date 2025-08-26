# GeminiSharp.Model.CachedContent

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | Optional. Identifier. The resource name of the CachedContent | [optional] 
**Model** | **string** | Immutable. The name of the Model to use for cached content | [optional] 
**SystemInstruction** | [**RequestContent**](RequestContent.md) |  | [optional] 
**Contents** | [**Collection&lt;RequestContent&gt;**](RequestContent.md) | Optional. Input only. Immutable. The content to cache | [optional] 
**Tools** | [**Collection&lt;Tool&gt;**](Tool.md) | Optional. Input only. Immutable. A list of Tools the Model may use to generate the next response | [optional] 
**ToolConfig** | [**ToolConfig**](ToolConfig.md) |  | [optional] 
**CreateTime** | **DateTime** | Output only. Creation time of the cache entry | [optional] 
**UpdateTime** | **DateTime** | Output only. When the cache entry was last updated | [optional] 
**UsageMetadata** | [**CachedContentUsageMetadata**](CachedContentUsageMetadata.md) |  | [optional] 
**ExpireTime** | **DateTime** | Timestamp in UTC of when this resource is considered expired | [optional] 
**Ttl** | **string** | Input only. New TTL for this resource, input only | [optional] 
**DisplayName** | **string** | Optional. Immutable. The user-generated meaningful display name of the cached content | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

