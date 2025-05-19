using PipeService.Contracts.Models;
using PipeService.Repositories.Models;
using Riok.Mapperly.Abstractions;

namespace PipeService.Mapper
{
    [Mapper]
    public static partial class GetPipeMapper
    {
        public static IEnumerable<PipeItem> ToPipeItem(
        this IEnumerable<DbPipeItem> pipeItems) =>
        pipeItems.Select(x => x.ToPipeItem());

        private static partial PipeItem ToPipeItem(this DbPipeItem dbReworkStationItem);
    }
}
