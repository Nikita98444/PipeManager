
using MediatR;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Queries;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class DeletePipeHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<DeletePipeQuery, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;

        public async Task<Response> Handle(DeletePipeQuery request, CancellationToken cancellationToken)
        {
                int rowsAffected = await _pipesRepository.InTransactionAsync(async tr => await _pipesRepository.DeletePipe(request.Id, tr));

                if (rowsAffected == 0)
                  return Response.IsError($"Запись с ID {request.Id} не найдена"); // Запись не была найдена/удалена
                

                return new DeletePipeResponse();
        }
    }
}
