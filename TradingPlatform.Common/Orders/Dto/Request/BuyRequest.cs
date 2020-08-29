using System;

namespace TradingPlatform.Common.Orders.Dto.Request
{
    /// <summary>
    /// Запрос на покупку.
    /// </summary>
    public sealed class BuyRequest
    {
        /// <summary>
        /// Идентификатор платежной сессии.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Номер карты.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// CSV код.
        /// </summary>
        public int CSV { get; set; }

        /// <summary>
        /// Дата действия карты.
        /// </summary>
        public DateTime DateAction { get; set; }

        /// <summary>
        /// Держатель карты.
        /// </summary>
        public string Wearer { get; set; }

        /// <summary>
        /// Покупатель.
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Id продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Сумма покупки.
        /// </summary>
        public decimal Cost { get; set; }
    }
}
