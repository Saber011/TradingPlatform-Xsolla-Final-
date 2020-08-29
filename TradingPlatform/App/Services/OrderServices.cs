using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingPlatform.App.ApiClient;
using TradingPlatform.App.Exceptions;
using TradingPlatform.App.Interfaces;
using TradingPlatform.App.Validation;
using TradingPlatform.Common;
using TradingPlatform.Common.Orders.Dto;
using TradingPlatform.Common.Orders.Dto.Request;

namespace TradingPlatform.App.Services
{
    /// <inheritdoc/>
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ApiClientTradingPlatform _apiClient;

        public OrderServices(
            IOrderRepository orderRepository,
            IEmailService emailService,
            IUserService userService,
            IProductService productService,
            ApiClientTradingPlatform apiClient)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _userService = userService;
            _productService = productService;
            _apiClient = apiClient;
        }

        /// <inheritdoc/>
        public async Task<List<OrderDto>> GetHistoryOrderAsync(int id)
        {
            return await _orderRepository.GetUserOrderAsync(id);
        }

        /// <inheritdoc/>
        public async Task<string> OrderAsync(OrderRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var product = await _productService.GetProductByIdAsync(request.ProductId);

            if (product == null || product.Count < 0)
            {
                throw new AppException("Невозможно купить данный товар");
            }

            return Guid.NewGuid().ToString();
        }

        /// <inheritdoc/>
        public async Task<bool> BuyAsync(BuyRequest request)
        {
            Guard.NotNull(request, nameof(request));

            if (!CardValidateLuhn(request.CardNumber) || !CvcValidate(request.CSV))
            {
                throw new AppException("Карта не валидна");
            }

            //Симуляция успешного пополнения
            var user = await _userService.GetUserAsync(request.IdUser);

            var key = await _orderRepository.BuyAsync(request.ProductId);

            await _orderRepository.CreateOrderAsync(request);

            var notification = JsonConvert.SerializeObject(new Notification()
            {
                ProductKey = key,
                UserName = user.Email,
                ProductId = request.ProductId,
                SessionId = request.SessionId
            });
            _ = Task.Run(async () => await _apiClient.Post(notification));

            _ = Task.Run(async() => await _emailService.SendEmailAsync(user.Email, "TradingPlatform покупка ключа", "Поздравляем с приобритением ключа" + key));

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> SetCommissionAsync(int commission)
        {
            Guard.CheckingRangeValues(commission, nameof(commission));
            
            await  _orderRepository.SetCommissionAsync(commission);

            return true;
        }

        private bool CardValidateLuhn(string nunber)
        {
            return nunber.All(char.IsDigit) && nunber.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                ? thisNum
                : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum))
                .Sum() % 10 == 0;
        }

        private bool CvcValidate(int cvc)
        {
            return 99 < cvc && cvc < 1000;
        }
    }
}
