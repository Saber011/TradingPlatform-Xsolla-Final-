using System;

namespace TradingPlatform.Common.Products.Dto.Request
{
    /// <summary>
    /// Продукты.
    /// </summary>
    [Serializable]
    public sealed class ProductRequest
    {
        /// <summary>
        /// Наименование продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Игровые ключи.
        /// </summary>
        public string[] GameKey { get; set; }
        
        /// <summary>
        /// Продавец.
        /// </summary>
        public int VendorId { get; set; }
    }
}
