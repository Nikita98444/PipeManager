namespace PipeManager.Client.Contracts.Models
{
    /// <summary>
    /// DTO для справочника марок стали
    /// </summary>
    public class SteelGradeItemDto
    {
        /// <summary>
        ///Ключ таблицы марок стали
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// Уникальное наименование марки стали
        /// </summary>
        public string GradeName { get; set; } = string.Empty;
    }
}
