
using MediatR;
using PipeCommon.Responses;

namespace PipeService.Contracts
{
    /// <summary>
    /// Редактировние трубы
    /// </summary>
    public class UpdatePipeRequest : IRequest<Response>
    {
        /// <summary>
        /// Уникальный идентификатор трубы в базе данных.
        /// </summary>
        public int PipeId { get; set; }

        /// <summary>
        /// Номер трубы (например, "T-001").
        /// </summary>
        public string PipeNumber { get; set; } = string.Empty;

        /// <summary>
        /// Флаг качества трубы: true - годная, false - брак.
        /// </summary>
        public bool IsGoodQuality { get; set; }


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
        /// Id пакета (может быть null, если труба не в пакете)
        /// </summary>
        public int? PackageId { get; set; }

        /// <summary>
        /// Id марки стали
        /// </summary>
        public int GradeId { get; set; }
    }
}
