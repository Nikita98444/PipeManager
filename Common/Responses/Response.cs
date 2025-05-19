using PipeCommon.Errors;

namespace PipeCommon.Responses;

public class Response : IResponse
{
    /// <summary>
    /// Ответ без ошибок
    /// </summary>
    public static Response IsOk(Response response) => response;

    /// <summary>
    /// Ответ со списком ошибок
    /// </summary>
    /// <param name="messageError">Описание ошибки</param>
    /// <param name="data">Массив сообщений об ошибках</param>
    /// <returns>Ответ на API-запрос</returns>
    public static Response IsError(string messageError, Dictionary<string, object> data = null)
        => new ErrorResponse(messageError, data);

    public static Response IsError(Dictionary<string, object> data = null)
        => new ErrorResponse("Ошибка", data);
}
