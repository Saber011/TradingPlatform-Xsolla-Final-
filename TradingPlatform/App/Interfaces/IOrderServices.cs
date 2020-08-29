using System.Collections.Generic;
using System.Threading.Tasks;
using TradingPlatform.Common.Orders.Dto;
using TradingPlatform.Common.Orders.Dto.Request;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Сервис для работы с заказами.
    /// </summary>
    public interface IOrderServices
    {
        /// <summary>
        /// Заказ.
        /// </summary>
        Task<string> OrderAsync(OrderRequest request);

        /// <summary>
        /// История заказов
        /// </summary>
        Task<List<OrderDto>> GetHistoryOrderAsync(int id);

        /// <summary>
        /// Покупка.
        /// </summary>
        Task<bool> BuyAsync(BuyRequest request);
        
        /// <summary>
        /// Установить коммисию площабки.
        /// </summary>
        Task<bool> SetCommissionAsync(int commission);
    }
}
