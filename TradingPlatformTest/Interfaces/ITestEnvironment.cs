using System;
using System.Net.Http;

namespace Parma.Gasps.Urp.Tests.IntegrationTests.Application
{
    /// <summary>
    /// Тестовое окружение.
    /// </summary>
    /// <remarks>
    /// Создание окружения производится через <see cref='IApplicationFactory.CreateEnvironment'>фабрику приложения</see>.
    /// </remarks>
    public interface ITestEnvironment
    {
        /// <summary>
        /// Создаёт `HTTP`-клиент.
        /// </summary>
        /// <param name="setupAction">Дополнительная настройка `HTTP`-клиента.</param>
        /// <example>
        /// <code>
        /// [Scenario]
        /// public void Test(HttpClient client)
        /// {
        ///     // Arrange
        ///     "Создание `HTTP`-клиента"
        ///         .x(() => client = _environment.CreateClient(options => ...));
        ///
        ///     ...
        /// }
        /// </code>
        /// </example>
        HttpClient CreateClient(Action<HttpClientOptions>? setupAction = null);
    }
}