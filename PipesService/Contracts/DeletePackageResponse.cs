
using PipeCommon.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Ответ на запрос "Удаление записи из таблицы пакетов"
    /// </summary>
    [SwaggerSchema(description: "Ответ на запрос \"Удаление записи из таблицы пакетов")]
    public class DeletePackageResponse : BaseResponse
    {
    }
}
