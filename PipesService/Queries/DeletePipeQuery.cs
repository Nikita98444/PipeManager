using MediatR;
using PipeCommon.Responses;

namespace PipeService.Queries
{
    public class DeletePipeQuery : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
