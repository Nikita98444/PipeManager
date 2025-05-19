
using PipeCommon.Responses;
using PipeService.Contracts.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Получение списка данных о марках стали
    /// </summary>
    [SwaggerSchema(description: "Получение списка данных о марках стали")]
    public class GetSteelGradesResponse : ResponseItems<SteelGradeItem>
    {
    }
}
