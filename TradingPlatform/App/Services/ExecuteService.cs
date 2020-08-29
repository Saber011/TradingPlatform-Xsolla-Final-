using Common.Enums;
using EasyCaching.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Infrastructure;

namespace TradingPlatform.Services
{
    /// <inheritdoc/>
    public sealed class ExecuteService : IExecuteService
    {
        private readonly IEasyCachingProviderFactory _managedCacheService;
        private readonly IEasyCachingProvider cachingProvider;
        private TimeSpan DefaultCacheLifetime { get; } = new TimeSpan(0, 1, 0);

        public ExecuteService(IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            _managedCacheService = easyCachingProviderFactory;
            cachingProvider = _managedCacheService.GetCachingProvider("redis1");
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<TResult>> TryExecuteWithCacheAsync<TResult>(Func<Task<TResult>> action, params object[] param)
        {
            var metodParamet = string.Join(",", param.Select(x => x.ToString()));
            var data = (await cachingProvider.GetAsync<ServiceResponse<TResult>>(metodParamet)).Value;

            if (data == null)
            {
                var result = await TryExecute(action);
                await cachingProvider.SetAsync(metodParamet, result, DefaultCacheLifetime);
                return result;
            }

            return data;
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<TResult>> TryExecute<TResult>(Func<Task<TResult>> action)
        {
            var result = new ServiceResponse<TResult>();
            try
            {
                TResult content = await action();

                result.Content = content;
                result.ResponseInfo.Status = ResponseStatus.Success;

                return result;
            }
            catch (ValidationException e)
            {
                result.ResponseInfo.Status = ResponseStatus.ValidationError;
                result.ResponseInfo.ErrorMessage = e.Message;
                result.ResponseInfo.ValidationErrors = e.ErrorsList;
                result.ResponseInfo.ErrorCode = ErrorCode.FailedValidation;

                return result;
            }

            catch (HttpResponseException e)
            {
                result.ResponseInfo.Status = ResponseStatus.ValidationError;
                result.ResponseInfo.ErrorMessage = e.Message;
                result.ResponseInfo.ErrorCode = ErrorCode.FailedValidation;

                return result;
            }

            catch (Exception e)
            {
                result.ResponseInfo.Status = ResponseStatus.Error;
                result.ResponseInfo.ErrorMessage = e.Message;
                result.ResponseInfo.ErrorCode = ErrorCode.Unclassified;

                return result;
            }
        }
    }
}
