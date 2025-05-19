

using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts.Models
{
    /// <summary>
    /// Модель данных (DTO) для представления информации о марках стали.
    /// </summary>
    [SwaggerSchema(Description = "Модель данных (DTO) для представления информации о марках стали")]
    public class SteelGradeItem
    {
        /// <summary>
        ///Ключ таблицы марок стали
        /// </summary>
        [SwaggerSchema(Description = "Ключ таблицы марок стали")]
        public int GradeId { get; set; }

        /// <summary>
        /// Уникальное наименование марки стали
        /// </summary>
        [SwaggerSchema(Description = "Уникальное наименование марки стали")]
        public string GradeName { get; set; } = string.Empty;
    }
}
