using PipeManager.Client.Contracts.Models;

namespace PipeManager.Client.Services.Abstractions
{
    public interface IPipeApiClient
    {
        /// <summary>
        /// Получает список труб из API.
        /// </summary>
        Task<ApiResult<List<PipeItemDto>>> GetPipesAsync();

        /// <summary>
        /// Удаляет трубу по её ID.
        /// </summary>
        Task<ApiResult<bool>> DeletePipeAsync(int pipeId);

        /// <summary>
        /// Получение списка марок стали
        /// </summary>
        Task<ApiResult<List<SteelGradeItemDto>>> GetSteelGradesAsync();

        /// <summary>
        /// Получение списка пакетов, в которые объединяются трубы
        /// </summary>
        Task<ApiResult<List<PackageItemDto>>> GetPackagesAsync();

        /// <summary>
        /// Удаляет пакет по её ID.
        /// </summary>
        Task<ApiResult<bool>> DeletePackageAsync(int packageId);

        /// <summary>
        /// Добавление трубы
        /// </summary>
        Task<ApiResult<bool>> AddPipeAsync(AddPipeItemDto pipeToAdd);

        /// <summary>
        /// Добавление пакета, для объединения труб
        /// </summary>
        Task<ApiResult<bool>> AddPackageAsync(AddPackageItemDto packageToAdd);

        /// <summary>
        /// Редактирование трубы
        /// </summary>
        Task<ApiResult<bool>> UpdatePipeAsync(PipeItemDto pipeToUpdate);

        /// <summary>
        /// Редактирование пакета, в которые объеденяют трубы
        /// </summary>
        Task<ApiResult<bool>> UpdatePackageAsync(PackageItemDto packageToUpdate);
    }
}
