namespace PipeCommon.Requests;

/// <summary>
/// Базовый запрос с фильтром и сортировкой
/// </summary>
[Serializable]
public class FilterOrderRequest : FilterRequest
{
    /// <summary>
    /// Сортировка (поле, направление)
    /// </summary>
    public KeyValuePair<string, string> OrderBy { get; set; }
}
