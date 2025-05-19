using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PipeService.Queries;
using Swashbuckle.AspNetCore.Annotations;
using static PipeManager.Helpers.ControllerHelper;
using PipeCommon.Errors;
using PipeService.Contracts;

namespace PipeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class PipesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Получить список труб с информацией о марке и пакете
        /// </summary>
        [HttpGet("GetPipes")]
        [ProducesResponseType(typeof(GetPipesResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Получить список труб с информацией о марке и пакете",
        OperationId = "GetPipes",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> GePipes()
        {
            return GetActionResult(await _mediator.Send(new GetPipesQuery()));
        }

        /// <summary>
        /// Удаление записи из таблицы труб
        /// </summary>
        [HttpDelete("DeletePipe")]
        [ProducesResponseType(typeof(DeletePipeResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Удаление записи из таблицы труб",
        OperationId = "DeletePipe",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> DeletePipe([FromQuery, SwaggerParameter("Ключ таблицы труб")] int pipeId)
        {
            return GetActionResult(await _mediator.Send(new DeletePipeQuery() { Id = pipeId }));
        }

        /// <summary>
        /// Получить список марок стали
        /// </summary>
        [HttpGet("GetSteelGrades")]
        [ProducesResponseType(typeof(GetSteelGradesResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Получить список марок стали",
        OperationId = "GetSteelGrades",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> GetSteelGrades()
        {
            return GetActionResult(await _mediator.Send(new GetSteelGradesQuery()));
        }

        /// <summary>
        /// Получить список пакетов, в которые объединяются трубы
        /// </summary>
        [HttpGet("GetPackages")]
        [ProducesResponseType(typeof(GetPackagesResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Получить список пакетов, в которые объединяются трубы",
        OperationId = "GetPackages",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> GetPackages()
        {
            return GetActionResult(await _mediator.Send(new GetPackagesQuery()));
        }

        /// <summary>
        /// Удаление записи из таблицы пакетов
        /// </summary>
        [HttpDelete("DeletePackage")]
        [ProducesResponseType(typeof(DeletePackageResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Удаление записи из таблицы пакетов",
        OperationId = "DeletePackage",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> DeletePackage([FromQuery, SwaggerParameter("Ключ таблицы пакетов")] int packageId)
        {
            return GetActionResult(await _mediator.Send(new DeletePackageQuery() { Id = packageId }));
        }

        /// <summary>
        /// Добавление пакета
        /// </summary>
        [HttpPost("AddPackage")]
        [ProducesResponseType(typeof(AddPackageResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Добавление пакета",
        OperationId = "AddPackage",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> AddPackage(
             [FromBody, SwaggerSchema("Данные для добавление нового пакета")] AddPackageRequest request)
        {
            return GetActionResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Редактирование трубы
        /// </summary>
        [HttpPost("UpdatePipe")]
        [ProducesResponseType(typeof(UpdatePipeResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Редактирование трубы",
        OperationId = "UpdatePipe",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> UpdatePipe(
             [FromBody, SwaggerSchema("Данные для редактирования записи трубы")] UpdatePipeRequest request)
        {
            return GetActionResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Добавление трубы
        /// </summary>
        [HttpPost("AddPipe")]
        [ProducesResponseType(typeof(AddPipeResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Добавление трубы",
        OperationId = "AddPipe",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> AddPipe(
             [FromBody, SwaggerSchema("Данные для добавление новой записи")] AddPipeRequest request)
        {
            return GetActionResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Редактирование пакета
        /// </summary>
        [HttpPost("UpdatePackage")]
        [ProducesResponseType(typeof(UpdatePackageResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [SwaggerOperation(
        Summary = "Редактирование пакета",
        OperationId = "UpdatePackage",
        Tags = ["Трубы"]
         )]
        public async Task<IActionResult> UpdatePackage(
             [FromBody, SwaggerSchema("Данные для редактирования записи пакета")] UpdatePackageRequest request)
        {
            return GetActionResult(await _mediator.Send(request));
        }
    }
}
