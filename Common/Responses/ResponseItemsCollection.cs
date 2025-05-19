using System.Text.Json.Serialization;

namespace PipeCommon.Responses
{
    /// <summary>
    /// Общий типизированный класс для ответов с определенной коллекцией элементов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
    [JsonDerivedType(typeof(PagingResponse<>))]
    public class ResponseItemsCollection<T> : BaseResponse
    {
        /// <summary>
        /// Коллекция элементов определенного типа
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
