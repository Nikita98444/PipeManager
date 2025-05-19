using MediatR;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Mapper;
using PipeService.Queries;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class GetPipesHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<GetPipesQuery, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;

        public async Task<Response> Handle(GetPipesQuery request, CancellationToken cancellationToken)
        {
            var dbCollection = await _pipesRepository.InTransactionAsync(async tr =>
               await _pipesRepository.GetPipes(tr));


            if (dbCollection == null)
                return Response.IsError("Не удалось загрузить информацию о трубам");

             return new GetPipesResponse
             {
                 Items = dbCollection.ToPipeItem()
             };
        }
    }
}
