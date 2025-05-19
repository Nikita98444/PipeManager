namespace PipeCommon.Requests;

/// <summary>
/// Базовый запрос с фильтром
/// </summary>
[Serializable]
public class FilterRequest
{
    /// <summary>
    /// Фильтры 
    /// </summary>
    public IDictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
}
