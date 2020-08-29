using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Common.Products.Dto;
using TradingPlatform.Common.Products.Dto.Request;
using TradingPlatform.Infrastructure;

namespace TradingPlatform.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IExecuteService _executeService;

        public ProductController(IProductService productService, IExecuteService executeService)
        {
            _productService = productService;
            _executeService = executeService;
        }

        /// <summary>
        /// Получить Все продукты.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        [Authorize]
        public async Task<ServiceResponse<List<ProductDto>>> GetProducts()
        {
            return await _executeService.TryExecuteWithCacheAsync(() => _productService.GetProductsAsync(), "AllProduct");
        }

        /// <summary>
        /// Добавить продукт.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [Authorize]
        public async Task<ServiceResponse<bool>> AddProduct(ProductRequest request)
        {
            return await _executeService.TryExecute(() => _productService.CreateProductAsync(request));
        }

        /// <summary>
        /// Удалить продукт.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpDelete]
        [Authorize(Roles ="Vendor")]
        public async Task<ServiceResponse<bool>> DeleteProduct(int id)
        {
            return await _executeService.TryExecute(() => _productService.DeleteProductAsync(id));
        }

        /// <summary>
        /// Изменить продукт.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPut]
        [Authorize(Roles = "Vendor")]
        public async Task<ServiceResponse<bool>> Updateroduct(UpdateProductRequest request)
        {
            return await _executeService.TryExecute(() => _productService.UpdateProductAsync(request));
        }

        /// <summary>
        /// Получить продукт по id.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ServiceResponse<ProductDto>> GetById(int id)
        {
            return await _executeService.TryExecuteWithCacheAsync(async () => await _productService.GetProductByIdAsync(id), "GetByIdProduct", id);
        }
    }
}
