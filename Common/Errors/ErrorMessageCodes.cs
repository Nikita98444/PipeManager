
namespace PipeCommon.Errors;

/// <summary>
/// Коды сообщений ошибок в словаре
/// </summary>
public enum ErrorMessageCodes
{
    /// <summary>
    /// Пустое поле
    /// </summary>
    EmptyField = 1,

    /// <summary>
    /// При выполнении операции возникла непредвиденная ошибка
    /// </summary>
    OperationFailed = 2,

    /// <summary>
    /// Число не входит в диапазон
    /// </summary>
    NotInRangeField = 3,

    /// <summary>
    /// Длина поля превышает возможную длину в таблице
    /// </summary>
    LengthField = 4,

    /// <summary>
    /// Поле не соответствует формату 0.0.0.0:1234
    /// </summary>
    FormatFieldIpAndPort = 5,

    /// <summary>
    /// Значение должно быть больше N
    /// </summary>
    ValueMustBeGreater = 6,

    /// <summary>
    /// Значение должно быть больше или равно N
    /// </summary>
    ValueMustBeGreaterOrEqual = 7,

    /// <summary>
    /// Значение должно быть меньше N
    /// </summary>
    ValueMustBeLess = 8,

    /// <summary>
    /// Значение должно быть меньше или равно N
    /// </summary>
    ValueMustBeLessOrEqual = 9,

    /// <summary>
    /// Количество должно быть больше N
    /// </summary>
    CountMustBeGreater = 10,

    /// <summary>
    /// Количество должно быть больше или равно N
    /// </summary>
    CountMustBeGreaterOrEqual = 11,

    /// <summary>
    /// Количество должно быть меньше N
    /// </summary>
    CountMustBeLess = 12,

    /// <summary>
    /// Количество должно быть меньше или равно N
    /// </summary>
    CountMustBeLessOrEqual = 13,
}