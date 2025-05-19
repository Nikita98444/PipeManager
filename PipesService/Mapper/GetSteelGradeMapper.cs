
using PipeService.Contracts.Models;
using PipeService.Repositories.Models;
using Riok.Mapperly.Abstractions;

namespace PipeService.Mapper
{
    [Mapper]
    public static partial class GetSteelGradeMapper
    {
        public static IEnumerable<SteelGradeItem> ToSteelGradeItem(
            this IEnumerable<DbSteelGradeItem> pipeItems) =>
            pipeItems.Select(x => x.ToSteelGradeItem());

        private static partial SteelGradeItem ToSteelGradeItem(this DbSteelGradeItem dbReworkStationItem);
    }
}
