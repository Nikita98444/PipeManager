
using MediatR;
using PipeCommon.Responses;

namespace PipeService.Contracts
{
    /// <summary>
    /// Редактировние пакета
    /// </summary>
    public class UpdatePackageRequest : IRequest<Response>
    {
        /// <summary>
        ///  ключ таблицы пакетов
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// Номер пакета, в который включена труба (например, "ПАКЕТ-001").
        /// </summary>
        public string PackageNumber { get; set; } = string.Empty;

        /// <summary>
        /// Дата формирования пакета, в который включена труба.
        /// </summary>
        public DateTime PackageDate { get; set; }
    }
}
