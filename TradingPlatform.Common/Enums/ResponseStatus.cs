namespace Common.Enums
{
    /// <summary>
    /// Статусы ответа
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Операция была успешно выполнена
        /// </summary>
        Success = 0,

        /// <summary>
        /// Операция была выполнена с ошибкой
        /// </summary>
        Error = 1,

        /// <summary>
        /// Операция была выполнена с ошибкой по причине некорректных входных данных (ошибки валидации прилагаются)
        /// </summary>
        ValidationError = 2,
    }
}
