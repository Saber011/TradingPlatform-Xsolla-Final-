namespace TradingPlatform.Common.Products.Dto.Request
{
    /// <summary>
    /// Обновление информации о продукте.
    /// </summary>
    public sealed class UpdateProductRequest
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
    }
}