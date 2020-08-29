using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Ocsp;
using TradingPlatform.App.Exceptions;
using TradingPlatform.App.Interfaces;
using TradingPlatform.App.Validation;
using TradingPlatform.Common.Products.Dto;
using TradingPlatform.Common.Products.Dto.Request;

namespace TradingPlatform.App.Services
{
    /// <inheritdoc/>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateProductAsync(ProductRequest request)
        {
            Guard.NotNull(request, nameof(request));

            if (request.Count != request.GameKey.Length)
            {
                throw new AppException("Количество ключей не совпадает с количеством товара");
            }

            await _productRepository.CreateProductAsync(request);

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProductAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));
            
            if (await _productRepository.GetProductByIdAsync(id) == null)
            {
                throw new AppException("Товар не найден");
            }

            await _productRepository.DeleteProductAsync(id);

            return true;
        }

        /// <inheritdoc/>
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));

            return await _productRepository.GetProductByIdAsync(id)
                   ?? throw  new AppException("Товар не найден");
        }

        /// <inheritdoc/>
        public async Task<List<ProductDto>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProductAsync(UpdateProductRequest request)
        {
            Guard.NotNull(request, nameof(request));
            
            if (await _productRepository.GetProductByIdAsync(request.Id) == null)
            {
                throw new AppException("Товар не найден");
            }

            await _productRepository.UpdateProductAsync(request);

            return true;
        }
    }
}
