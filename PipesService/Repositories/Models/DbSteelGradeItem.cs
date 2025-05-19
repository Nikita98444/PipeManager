

namespace PipeService.Repositories.Models
{
    /// <summary>
    /// Справочник марок стали
    /// </summary>
    public class DbSteelGradeItem
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
