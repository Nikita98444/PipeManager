
using MediatR;
using Npgsql;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class AddPackageHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<AddPackageRequest, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;
        public async Task<Response> Handle(AddPackageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rowsAffected = await _pipesRepository.InTransactionAsync(async tr =>
                   await _pipesRepository.AddPackage(request, tr));

                return new AddPackageResponse();
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                return Response.IsError($"Нарушение уникальности в Базе данных");
            }
        }
    }
}
