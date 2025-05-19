namespace PipeCommon.Responses;

/// <summary>
/// Общий типизированный класс для ответов с определенной коллекцией элементов
/// </summary>
[Serializable]
public class ResponseItems<T> : Response
{
    /// <summary>
    /// Коллекция элементов определенного типа
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}
