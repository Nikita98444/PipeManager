
using PipeCommon.Responses;
using PipeService.Contracts.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{    
    /// <summary>
     /// Получение списка данных о трубах
     /// </summary>
    [SwaggerSchema(description: "Получение списка данных о трубах")]
    public class GetPipesResponse : ResponseItems<PipeItem>
    {
    }
}
