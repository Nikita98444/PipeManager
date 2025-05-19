

using MediatR;
using PipeCommon.Responses;

namespace PipeService.Queries
{
    public class DeletePackageQuery : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
