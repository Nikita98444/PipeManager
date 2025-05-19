
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace PipeService.Contracts.Models
{
    /// <summary>
    /// Модель данных (DTO) для представления информации о трубе.
    /// </summary>
    [SwaggerSchema(Description = "Модель данных (DTO) для представления информации о трубе")]
    public class PipeItem
    {
        /// <summary>
        /// Уникальный идентификатор трубы в базе данных.
        /// </summary>
        [SwaggerSchema(description: "Уникальный идентификатор трубы в базе данных")]
        public int PipeId { get; set; }

        /// <summary>
        /// Номер трубы (например, "T-001").
        /// </summary>
        [SwaggerSchema(description: "Номер трубы (например, \"T-001\").")]
        public string PipeNumber { get; set; } = string.Empty;

        /// <summary>
        /// Флаг качества трубы: true - годная, false - брак.
        /// </summary>
        [SwaggerSchema(description: "Флаг качества трубы: true - годная, false - брак.")]
        public bool IsGoodQuality { get; set; }

        /// <summary>
        /// id марки стали.
        /// </summary>
        [SwaggerSchema(description: "id марки стали.")]
        public int GradeId { get; set; }

        /// <summary>
        /// Наименование марки стали (например, "Ст3сп", "09Г2С").
        /// </summary>
        [SwaggerSchema(description: "Наименование марки стали (например, \"Ст3сп\", \"09Г2С\").")]
        public string GradeName { get; set; } = string.Empty;

        /// <summary>
        /// Длина трубы в метрах.
        /// </summary>
        [SwaggerSchema(description: "Длина трубы в метрах.")]
        public double LengthMeters { get; set; }

        /// <summary>
        /// Диаметр трубы в миллиметрах.
        /// </summary>
        [SwaggerSchema(description: "Диаметр трубы в миллиметрах.")]
        public double DiameterMm { get; set; }

        /// <summary>
        /// Вес трубы в килограммах.
        /// </summary>
        [SwaggerSchema(description: "Вес трубы в килограммах.")]
        public double WeightKg { get; set; }

        /// <summary>
        /// id пакета (может быть null).
        /// </summary>
        [SwaggerSchema(description: "id пакета (может быть null).")]
        public int? PackageId { get; set; }

        /// <summary>
        /// Номер пакета, в который включена труба (например, "ПАКЕТ-001").
        /// Может быть null, если труба не входит в пакет.
        /// </summary>
        [SwaggerSchema(description: "Номер пакета, в который включена труба (например, \"ПАКЕТ-001\")")]
        public string? PackageNumber { get; set; }

        /// <summary>
        /// Дата формирования пакета, в который включена труба.
        /// Может быть null, если труба не входит в пакет.
        /// </summary>
        [SwaggerSchema(description: "Дата формирования пакета, в который включена труба")]
        public DateTime? PackageDate { get; set; }

        /// <summary>
        /// Вспомогательное свойство для отображения качества текстом.
        /// </summary>
        [JsonIgnore] // Не участвует в сериализации/десериализации
        public string QualityDisplay => IsGoodQuality ? "Годная" : "Брак";
    }
}
