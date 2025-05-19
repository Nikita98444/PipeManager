using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Mapper;
using PipeService.Queries;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class GetSteelGradesHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<GetSteelGradesQuery, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;
        public async Task<Response> Handle(GetSteelGradesQuery request, CancellationToken cancellationToken)
        {
            var dbCollection = await _pipesRepository.InTransactionAsync(async tr =>
               await _pipesRepository.GetSteelGrades(tr));


            if (dbCollection == null)
                return Response.IsError("Не удалось загрузить информацию о марках стали");

            return new GetSteelGradesResponse
            {
                Items = dbCollection.ToSteelGradeItem()
            };
        }
    }
}
