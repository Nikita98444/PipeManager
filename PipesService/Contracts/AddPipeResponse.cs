
using PipeCommon.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Ответ на запрос "Добавление трубы"
    /// </summary>
    [SwaggerSchema(description: "Ответ на запрос \"Добавление трубы\"")]
    public class AddPipeResponse : BaseResponse
    {
    }
}
