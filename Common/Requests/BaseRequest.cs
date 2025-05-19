using MediatR;
using System.Text.Json.Serialization;

namespace PipeCommon.Requests;

/// <summary>
/// Базовый класс для запросов
/// </summary>
[Serializable]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor, TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(FilterOrderRequest))]
[JsonDerivedType(typeof(FilterRequest))]
[JsonDerivedType(typeof(PagingRequest))]
public class BaseRequest : IRequest
{
}

/// <summary>
/// Базовый класс для запросов
/// </summary>
[Serializable]
public class BaseRequest<T> : IRequest<T> where T : class
{
}
