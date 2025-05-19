
using PipeCommon.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Ответ на запрос "Редактирование трубы"
    /// </summary>
    [SwaggerSchema(description: "Ответ на запрос \"Редактирование трубы\"")]
    public class UpdatePipeResponse : BaseResponse
    {
    }
}
