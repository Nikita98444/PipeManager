
using PipeCommon.Responses;
using PipeService.Contracts.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts
{
    /// <summary>
    /// Получение списка данных о пакетах, в которые объединяются трубы
    /// </summary>
    [SwaggerSchema(description: "Получение списка данных о пакетах, в которые объединяются трубы")]
    public class GetPackagesResponse : ResponseItems<PackageItem>
    {
    }
}
