

using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts.Models
{
    /// <summary>
    /// Модель данных (DTO) для представления информации о пакетах, в которые объединяются трубы.
    /// </summary>
    [SwaggerSchema(Description = "Модель данных (DTO) для представления информации о пакетах, в которые объединяются трубы")]
    public class PackageItem
    {
        /// <summary>
        ///  ключ таблицы пакетов
        /// </summary>
        [SwaggerSchema(Description = "ключ таблицы пакетов")]
        public int PackageId { get; set; }

        /// <summary>
        /// Номер пакета, в который включена труба (например, "ПАКЕТ-001").
        /// </summary>
        [SwaggerSchema(Description = "Номер пакета, в который включена труба (например, \"ПАКЕТ-001\").")]
        public string PackageNumber { get; set; } = string.Empty;

        /// <summary>
        /// Дата формирования пакета, в который включена труба.
        /// </summary>
        [SwaggerSchema(Description = "Дата формирования пакета, в который включена труба.")]
        public DateTime PackageDate { get; set; }
    }
}
