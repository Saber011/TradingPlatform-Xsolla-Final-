using System;

namespace TradingPlatform.Common.Orders.Dto
{
    /// <summary>
    /// Информация о заказе.
    /// </summary>
    [Serializable]
    public sealed class OrderDto
    {
        /// <summary>
        /// Id покупателя.
        /// </summary>
        public  int IdUser { get; set; }
        
        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string ProductName { get; set; }
        
        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Cost { get; set; }
        
        /// <summary>
        /// Коммисия.
        /// </summary>
        public  int Commission { get; set; }
    }
}
