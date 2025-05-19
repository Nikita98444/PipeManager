
namespace PipeCommon.Errors;

/// <summary>
/// Коды ошибок
/// </summary>
public enum ErrorCodes
{
    Ok = 0,

    /// <summary>
    /// Общая ошибка
    /// </summary>
    Error = 1,

    /// <summary>
    /// Показ сообщения
    /// </summary>
    ShowMessage = 2,

    /// <summary>
    /// При отправке запроса произошла ошибка
    /// </summary>
    SendFailed = 3,

    /// <summary>
    /// Ответ на запрос не пришел вовремя
    /// </summary>
    Timeout = 4,

    /// <summary>
    /// Код отдается, если токен устарел или оказался некорректным.
    /// </summary>
    InvalidToken = 5,

    /// <summary>
    /// Ошибка, обнаруженная в хэндлере
    /// </summary>
    HandlerError = 6,

    /// <summary>
    /// Код отдается, если валидация не прошла
    /// </summary>
    ValidationError = 7,
}