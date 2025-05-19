
using PipeCommon.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Ответ на запрос "Редактирование пакета"
    /// </summary>
    [SwaggerSchema(description: "Ответ на запрос \"Редактирование пакета\"")]
    public class UpdatePackageResponse : BaseResponse
    {
    }
}
