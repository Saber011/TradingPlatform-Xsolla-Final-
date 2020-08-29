using System;
using System.Threading.Tasks;
using TradingPlatform.Infrastructure;

namespace TradingPlatform.App.Interfaces
{
    /// <summary>
    /// Вспомогательный класс, который позволяет централизовано перехыватывать ошибки и оборачивать их
    /// в корректный ответ <see cref="ServiceResponse{T}"/>
    /// </summary>
    public interface IExecuteService
    {
        /// <summary>
        /// Выполнить операцию с кешированием.
        /// </summary>
        Task<ServiceResponse<TResult>> TryExecuteWithCacheAsync<TResult>(Func<Task<TResult>> action, params object[] param);

        /// <summary>
        /// Выполнить какое либо действие и обернуть резудьтат в <see cref="ServiceResponse{T}"/>
        /// </summary>
        Task<ServiceResponse<TResult>> TryExecute<TResult>(Func<Task<TResult>> action);
    }
}
