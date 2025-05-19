namespace PipeCommon.Responses;

/// <summary>
/// Ответ на запрос с пагинацией
/// </summary>
[Serializable]
public class PagingResponse<T> : ResponseItems<T>
{
    private readonly int? _pageSize;
    private int? _totalCount;

    /// <summary>
    /// Страница
    /// </summary>
    public int? PageNumber { get; set; } = 1;

    /// <summary>
    /// Число страниц
    /// </summary>
    public int? PageCount
    {
        get
        {
            if (_pageSize.HasValue && TotalCount.HasValue)
            {
                return _pageSize == 0 || TotalCount == 0
                    ? 1
                    : (int)Math.Ceiling((decimal)TotalCount / _pageSize.Value);
            }
            else
            {
                return 1;
            }
        }
    }

    /// <summary>
    /// Oбщее количество найденных записей
    /// </summary>
    public int? TotalCount
    {
        get => _totalCount;
        set => _totalCount = value < 0 ? 0 : value;
    }

    /// <summary>
    /// Размер страницы
    /// </summary>
    /// <param name="pageSize"></param>
    public PagingResponse(int? pageSize)
    {
        _pageSize = pageSize;
    }

    /// <summary>
    /// Конструктор, необходимый для успешной сериализации System.Text.Json
    /// </summary>
    public PagingResponse() { }
}
