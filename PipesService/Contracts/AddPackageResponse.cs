
using PipeCommon.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Ответ на запрос "Добавление пакета"
    /// </summary>
    [SwaggerSchema(description: "Ответ на запрос \"Добавление пакета\"")]
    public class AddPackageResponse : BaseResponse
    {
    }
}
