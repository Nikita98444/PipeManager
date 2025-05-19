
using MediatR;
using Npgsql;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class AddPipesHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<AddPipeRequest, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;
        public async Task<Response> Handle(AddPipeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rowsAffected = await _pipesRepository.InTransactionAsync(async tr =>
                   await _pipesRepository.AddPipe(request, tr));

                return new AddPipeResponse();
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                return Response.IsError($"Нарушение уникальности в Базе данных");
            }
        }
    }
}
