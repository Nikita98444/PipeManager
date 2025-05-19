



namespace PipeService.Repositories.Models
{
    /// <summary>
    /// Представляет объединенные данные по одной трубе, включая марку стали и информацию о пакете (если есть).
    /// Используется для отображения списка труб.
    /// </summary>
    public class DbPipeItem
    {
        /// <summary>
        /// Уникальный идентификатор трубы в базе данных.
        /// </summary>
        public int PipeId { get; set; }

        /// <summary>
        /// id пакета (может быть null).
        /// </summary>
        public int? PackageId { get; set; }

        /// <summary>
        /// Заводской или учетный номер трубы (например, "T-001").
        /// </summary>
        public string PipeNumber { get; set; } = string.Empty;

        /// <summary>
        /// Флаг качества трубы: true - годная, false - брак.
        /// </summary>
        public bool IsGoodQuality { get; set; }

        /// <summary>
        /// id марки стали.
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// Наименование марки стали (например, "Ст3сп", "09Г2С").
        /// </summary>
        public string GradeName { get; set; } = string.Empty;

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
        /// Номер пакета, в который включена труба (например, "ПАКЕТ-001").
        /// Может быть null, если труба не входит в пакет.
        /// </summary>
        public string? PackageNumber { get; set; }

        /// <summary>
        /// Дата формирования пакета, в который включена труба.
        /// Может быть null, если труба не входит в пакет.
        /// </summary>
        public DateTime? PackageDate { get; set; }
    }
}
