using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Common.Orders.Dto;
using TradingPlatform.Common.Orders.Dto.Request;
using TradingPlatform.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace TradingPlatform.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IExecuteService _executeService;

        public OrderController(IExecuteService executeService, IOrderServices orderServices)
        {
            _orderServices = orderServices;
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
        public async Task<ServiceResponse<List<OrderDto>>> GetOrderHistory(int id)
        {
            return await _executeService.TryExecuteWithCacheAsync(() => _orderServices.GetHistoryOrderAsync(id), "History", id);
        }

        /// <summary>
        /// Добавить продукт.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [Authorize]
        public async Task<ServiceResponse<string>> Order(OrderRequest request)
        {
            return await _executeService.TryExecute(() => _orderServices.OrderAsync(request));
        }

        /// <summary>
        /// Покупка.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost("Buy")]
        [Authorize]
        public async Task<ServiceResponse<bool>> Buy(BuyRequest request)
        {
            return await _executeService.TryExecute(() => _orderServices.BuyAsync(request));
        }
        
        /// <summary>
        /// Установить процент коммисси.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost("Commission")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResponse<bool>> Commission(int commission)
        {
            return await _executeService.TryExecute(() => _orderServices.SetCommissionAsync(commission));
        }
    }
}
