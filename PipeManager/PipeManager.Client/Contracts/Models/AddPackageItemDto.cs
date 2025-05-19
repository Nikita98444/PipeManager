namespace PipeManager.Client.Contracts.Models
{
    /// <summary>
    /// DTP для добавление пакета, для объединения труб
    /// </summary>
    public class AddPackageItemDto
    {
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
