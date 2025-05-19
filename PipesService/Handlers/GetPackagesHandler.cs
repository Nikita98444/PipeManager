using System;
using System.Collections.Generic;
using System.Linq;

using MediatR;
using PipeCommon.Responses;
using PipeService.Contracts;
using PipeService.Mapper;
using PipeService.Queries;
using PipeService.Repositories;

namespace PipeService.Handlers
{
    public class GetPackagesHandler(
            IPipesRepository pipesRepository,
            IMediator mediator
        ) : IRequestHandler<GetPackagesQuery, Response>
    {
        private readonly IPipesRepository _pipesRepository = pipesRepository;
        private readonly IMediator _mediator = mediator;

        public async Task<Response> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
        {
            var dbCollection = await _pipesRepository.InTransactionAsync(async tr =>
               await _pipesRepository.GetPackages(tr));


            if (dbCollection == null)
                return Response.IsError("Не удалось загрузить информацию о пакетах");

            return new GetPackagesResponse
            {
                Items = dbCollection.ToPackageItem()
            };
        }
    }
}
