using System.Text.Json.Serialization;

namespace PipeCommon.Responses;

/// <summary>
/// Базовый ответ
/// </summary>
[Serializable]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor, TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(ResponseItemsCollection<>))]
public class BaseResponse : Response
{

}
