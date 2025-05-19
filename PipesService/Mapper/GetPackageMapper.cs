
using PipeService.Contracts.Models;
using PipeService.Repositories.Models;
using Riok.Mapperly.Abstractions;

namespace PipeService.Mapper
{
    [Mapper]
    public static partial class GetPackageMapper
    {
        public static IEnumerable<PackageItem> ToPackageItem(
        this IEnumerable<DbPackageItem> pipeItems) =>
        pipeItems.Select(x => x.ToPackageItem());

        private static partial PackageItem ToPackageItem(this DbPackageItem dbReworkStationItem);
    }
}
