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
        HttpClient CreateClient();
    }
}