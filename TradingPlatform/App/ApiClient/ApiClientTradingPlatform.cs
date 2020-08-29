using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingPlatform.App.ApiClient
{
    /// <summary>
    /// Отправить пост запрос.
    /// </summary>
    public sealed class ApiClientTradingPlatform
    {
        private readonly HttpClient _httpClient;

        public ApiClientTradingPlatform(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Отправить пост запрос.
        /// </summary>
        public async Task<string> Post(string json)
        {
            HttpContent content = new StringContent(json);
            var response = await _httpClient.PostAsync("Send", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }

            throw new Exception(responseString);
        }
    }
}
