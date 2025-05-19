
namespace PipeCommon.Errors
{
    /// <summary>
    /// Сообщения об ошибках
    /// </summary>
    [Serializable]
    public static class ErrorMessages
    {
        /// <summary>
        /// Словарь код ошибки - текст ошибки
        /// </summary>
        private static Dictionary<ErrorMessageCodes, string> _messageDictionary = new Dictionary<ErrorMessageCodes, string>()
        {
            { ErrorMessageCodes.EmptyField, "Поле должно быть заполнено"},
            { ErrorMessageCodes.OperationFailed, "При выполнении операции возникла непредвиденная ошибка. Попробуйте повторить действие позже."},
            { ErrorMessageCodes.NotInRangeField, "Укажите значение от {0} до {1}"},
            { ErrorMessageCodes.LengthField, "Длина поля не более {0} символов"},
            { ErrorMessageCodes.FormatFieldIpAndPort, "Укажите значение, соответствующее формату 0.0.0.0:1234"},
            { ErrorMessageCodes.ValueMustBeGreater, "Значение должно быть больше {0}"},
            { ErrorMessageCodes.ValueMustBeGreaterOrEqual, "Значение должно быть больше или равно {0}"},
            { ErrorMessageCodes.ValueMustBeLess, "Значение должно быть меньше {0}"},
            { ErrorMessageCodes.ValueMustBeLessOrEqual, "Значение должно быть меньше или равно {0}"},
            { ErrorMessageCodes.CountMustBeGreater, "Количество должно быть больше {0}"},
            { ErrorMessageCodes.CountMustBeGreaterOrEqual, "Количество должно быть больше или равно {0}"},
            { ErrorMessageCodes.CountMustBeLess, "Количество должно быть меньше {0}"},
            { ErrorMessageCodes.CountMustBeLessOrEqual, "Количество должно быть меньше или равно {0}"},
        };

        /// <summary>
        /// Получить текст сообщения об ошибке
        /// </summary>
        /// <param name="code">Код ошибки</param>
        /// <param name="parameters">параметры для сообщения</param>
        public static string GetMessage(ErrorMessageCodes code, string[] parameters = null) =>
            string.Format(_messageDictionary[code], parameters ?? Array.Empty<string>());
    }
}
