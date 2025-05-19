using System.Data;
using PipeCommon.Database;
using PipeService.Contracts;
using PipeService.Repositories.Models;

namespace PipeService.Repositories
{
    public interface IPipesRepository : IBaseRepository
    {
        /// <summary>
        /// Получение списка информации по трубам
        /// </summary>
        Task<IEnumerable<DbPipeItem>> GetPipes(IDbTransaction transaction);

        /// <summary>
        /// Удаление записи из таблицы труб
        /// </summary>
        Task<int> DeletePipe(int Id, IDbTransaction transaction);

        /// <summary>
        /// Удаление записи из таблицы пакетов
        /// </summary>
        Task<int> DeletePackage (int Id, IDbTransaction transaction);

        /// <summary>
        /// получение списка марок стали
        /// </summary>
        Task<IEnumerable<DbSteelGradeItem>> GetSteelGrades(IDbTransaction transaction);

        /// <summary>
        /// получение списока пакетов, в которые объединяются трубы
        /// </summary>
        Task<IEnumerable<DbPackageItem>> GetPackages(IDbTransaction transaction);

        /// <summary>
        /// Добавление трубы
        /// </summary>
        Task<int> AddPipe(AddPipeRequest request, IDbTransaction transaction);

        /// <summary>
        /// Редактирование трубы
        /// </summary>
        Task<int> UpdatePipe (UpdatePipeRequest request, IDbTransaction transaction);

        /// <summary>
        /// Добавление пакета
        /// </summary>
        Task<int> AddPackage(AddPackageRequest request, IDbTransaction transaction);

        /// <summary>
        /// Редактирование пакета
        /// </summary>
        Task<int> UpdatePackage(UpdatePackageRequest request, IDbTransaction transaction);
    }
}
