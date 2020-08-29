namespace Common.Enums
{
    /// <summary>
    /// Коды ошибок
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Объект не был найден
        /// </summary>
        NotFound = 0,

        /// <summary>
        /// Ошибка на уровне БД
        /// </summary>
        _contextError = 1,

        /// <summary>
        /// Не правильный объект запроса
        /// </summary>
        WrongRequest = 2,

        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        Unclassified = 3,

        /// <summary>
        /// Авторизация не выполнена
        /// </summary>
        UnAuthorized = 4,

        /// <summary>
        /// Ошибка валидации
        /// </summary>
        FailedValidation = 5,

        /// <summary>
        /// Ошибка в логике выполнения 
        /// </summary>
        LogicValidation = 6,
    }
}
