using System.Text.Json.Serialization;

namespace PipeManager.Client.Contracts.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) для отображения элемента в списке труб.
    /// </summary>
    public class PipeItemDto
    {
        /// <summary>
        /// Уникальный идентификатор трубы.
        /// </summary>
        public int PipeId { get; set; }

        /// <summary>
        /// Уникальный номер трубы.
        /// </summary>
        public string? PipeNumber { get; set; }

        /// <summary>
        /// Качество трубы (true - годная, false - брак).
        /// </summary>
        public bool IsGoodQuality { get; set; }

        /// <summary>
        /// id марки стали.
        /// </summary>
        public int GradeId { get; set; }
        /// <summary>
        /// Наименование марки стали.
        /// </summary>
        public string? GradeName { get; set; }

        /// <summary>
        /// Длина трубы в метрах.
        /// </summary>
        public double LengthMeters { get; set; }

        /// <summary>
        /// Диаметр трубы в миллиметрах.
        /// </summary>
        public double DiameterMm { get; set; }

        /// <summary>
        /// Вес трубы в килограммах.
        /// </summary>
        public double WeightKg { get; set; }

        /// <summary>
        /// id пакета (может быть null).
        /// </summary>
        public int? PackageId { get; set; }

        /// <summary>
        /// Номер пакета (может быть null).
        /// </summary>
        public string? PackageNumber { get; set; }

        /// <summary>
        /// Дата формирования пакета (может быть null).
        /// </summary>
        public DateTime? PackageDate { get; set; }

        /// <summary>
        /// Вспомогательное свойство для отображения качества текстом.
        /// </summary>
        [JsonIgnore] // Не участвует в сериализации/десериализации
        public string QualityDisplay => IsGoodQuality ? "Годная" : "Брак";
    }
}
