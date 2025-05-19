using MediatR;
using Npgsql;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class UpdatePackageHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<UpdatePackageRequest, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;

        public async Task<Response> Handle(UpdatePackageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rowsAffected = await _pipesRepository.InTransactionAsync(async tr =>
                   await _pipesRepository.UpdatePackage(request, tr));

                if (rowsAffected == 0)
                    return Response.IsError($"Запись с ID {request.PackageId} не найдена"); // Запись не была найдена

                return new UpdatePackageResponse();
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                return Response.IsError($"Нарушение уникальности в Базе данных");
            }
        }

    }
}
