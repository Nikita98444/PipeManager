using System.Text.Json.Serialization;
using PipeCommon.Responses;

namespace PipeCommon.Errors;

/// <summary>
/// Реализованные статусы ошибок. Соответствуют кодам ответа HTTP
/// </summary>
public enum ErrorStatus
{
    OK = 200,
    BadRequestObject = 400,
    Unauthorized = 401,
    Forbid = 403,
    Timeout = 408,
    ServerError = 500
}

/// <summary>
/// Ответ с ошибкой
/// </summary>
[Serializable]
public class ErrorResponse : Response
{
    public const string ResponseTypeCode = "Error";

    public ErrorResponse()
    { }

    public ErrorResponse(string messageError, Dictionary<string, object> data = null)
    {
        Message = messageError;
        ErrorStatus = ErrorStatus.BadRequestObject;
        Data = data;
    }

    /// <summary>
    /// Статус ошибки
    /// </summary>
    [JsonIgnore]
    public ErrorStatus ErrorStatus { get; }

    /// <summary>
    /// Описание ошибки
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Список сообщений об ошибках
    /// </summary>
    public Dictionary<string, object> Data { get; set; }
}
