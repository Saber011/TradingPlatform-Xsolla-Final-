using Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace TradingPlatform.Infrastructure
{
    /// <summary>
    /// Generic HTTP response
    /// </summary>
    [Serializable]
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Public constructor
        /// </summary>
        public ServiceResponse()
        {
            ResponseInfo = new ResponseInfo
            {
                Status = ResponseStatus.Success,
            };
        }

        /// <summary>
        /// Содержимое ответа
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// Информация об ответе
        /// </summary>
        [Required]
        public ResponseInfo ResponseInfo { get; set; }
    }
}
