namespace PipeCommon.Requests;

/// <summary>
/// Базовый запрос с пагинацией
/// </summary>
[Serializable]
public class PagingRequest : FilterOrderRequest
{
    /// <summary>
    /// Страница 
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Кол-во записей на странице 
    /// </summary>
    public int? MaxRowCount { get; set; }
}
