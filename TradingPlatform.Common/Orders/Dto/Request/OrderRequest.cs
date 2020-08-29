using System.ComponentModel.DataAnnotations;

namespace TradingPlatform.Common.Orders.Dto.Request
{
    /// <summary>
    /// Запрос на заказ.
    /// </summary>
    public sealed class OrderRequest
    {
        /// <summary>
        /// Id продукта.
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Email покупателя.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Сумма покупки.
        /// </summary>
        public decimal Cost { get; set; }
    }
}
