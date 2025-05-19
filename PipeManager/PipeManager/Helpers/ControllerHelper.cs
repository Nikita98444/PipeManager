using Microsoft.AspNetCore.Mvc;
using PipeCommon.Errors;
using PipeCommon.Responses;

namespace PipeManager.Helpers
{
    /// <summary>
    /// Дополнительные методы для контроллеров
    /// </summary>
    public static class ControllerHelper
    {
        /// <summary>
        /// Получение результата для контроллера
        /// </summary>
        public static IActionResult GetActionResult(IResponse response)
        {
            if (response is ErrorResponse error)
            {
                return error.ErrorStatus == ErrorStatus.BadRequestObject
                    ? new BadRequestObjectResult(error)
                    : new ObjectResult(error.Message)
                    {
                        StatusCode = GetStatusCode(error.ErrorStatus)
                    };
            }
            else
            {
                return new OkObjectResult(response);
            }
        }
        private static int GetStatusCode(ErrorStatus errorStatus)
            => errorStatus switch
            {
                ErrorStatus.OK => StatusCodes.Status200OK,
                ErrorStatus.Forbid => StatusCodes.Status403Forbidden,
                ErrorStatus.BadRequestObject => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status303SeeOther,
            };
    }
}
