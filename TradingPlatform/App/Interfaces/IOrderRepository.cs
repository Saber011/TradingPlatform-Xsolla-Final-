using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlatform.Common.Orders.Dto;
using TradingPlatform.Common.Orders.Dto.Request;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с заказами.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// История заказов пользователя.
        /// </summary>
        Task<List<OrderDto>> GetUserOrderAsync(int id);

        /// <summary>
        /// Покупка товара.
        /// </summary>
        Task<string> BuyAsync(int id);
        
        /// <summary>
        /// Добавления заказа.
        /// </summary>
        Task CreateOrderAsync(BuyRequest request);
        
        /// <summary>
        /// Установить коммисию площабки.
        /// </summary>
        Task SetCommissionAsync(int commission);
    }
}
