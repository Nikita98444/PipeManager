
using PipeCommon.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Ответ на запрос "Удаление записи из таблицы труб"
    /// </summary>
    [SwaggerSchema(description: "Ответ на запрос \"Удаление записи из таблицы труб")]
    public class DeletePipeResponse : BaseResponse
    {
    }
}
