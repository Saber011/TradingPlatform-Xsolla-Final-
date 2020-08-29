using Common.Enums;
using System;

namespace TradingPlatform.Infrastructure
{
    /// <summary>
    /// Информация об ответе
    /// </summary>
    [Serializable]
    public class ResponseInfo
    {
        /// <summary>
        /// Статус ответа. Возможные значения:
        /// (0) Success - Операция была успешно выполнена
        /// 1 Error - Операция была выполнена с ошибкой
        /// 2 ValidationError - Операция была выполнена с ошибкой по причине некорректных входных данных 
        /// </summary>
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// Код ошибки. Возможные значения:
        /// (0) NotFound - Объект не был найден,
        /// (1) _contextError - Ошибка на уровне БД
        /// (2) WrongRequest - Не правильный объект запроса
        /// (3) Unclassified - Неизвестная ошибка
        /// (4) UnAuthorized - Авторизация не выполнена
        /// (5) FailedValidation - Ошибка валидации
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public ErrorsList ValidationErrors { get; set; }

        /// <summary>
        /// Идентификатор ошибки из службы регистрации ошибок
        /// </summary>
        public string ErrorContextInfoId { get; set; }
    }
}