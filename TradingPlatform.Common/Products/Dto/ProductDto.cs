using System;

namespace TradingPlatform.Common.Products.Dto
{
    /// <summary>
    /// Продукты.
    /// </summary>
    [Serializable]
    public sealed class ProductDto
    {
        /// <summary>
        /// Id продукта.
        /// </summary>
        public int Id { get; set; }

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
        /// Id продавца.
        /// </summary>
        public int VendorId { get; set; }
    }
}
