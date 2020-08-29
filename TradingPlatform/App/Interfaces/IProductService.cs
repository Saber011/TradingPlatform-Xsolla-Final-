using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlatform.Common.Products.Dto;
using TradingPlatform.Common.Products.Dto.Request;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Сервис для работы с продуктами.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Создание товара.
        /// </summary>
        Task<bool> CreateProductAsync(ProductRequest request);

        /// <summary>
        /// Удаление товара.
        /// </summary>
        Task<bool> DeleteProductAsync(int id);

        /// <summary>
        /// Изменнеие товара.
        /// </summary>
        Task<bool> UpdateProductAsync(UpdateProductRequest request);

        /// <summary>
        /// Получить список всех товаров.
        /// </summary>
        Task<List<ProductDto>> GetProductsAsync();

        /// <summary>
        /// Получить товар по ID.
        /// </summary>
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}
