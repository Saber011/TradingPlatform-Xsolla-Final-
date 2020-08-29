using System;
using System.Net;
using System.Net.Http;

namespace Parma.Gasps.Urp.Tests.IntegrationTests.Application
{
    /// <inheritdoc />
    public sealed class TestEnvironment : ITestEnvironment
    {
        private readonly ApplicationFactory _applicationFactory;
        private readonly string _controller;

        /// <summary>
        /// Конструктор <see cref="TestEnvironment"/>.
        /// </summary>
        /// <param name="applicationFactory">Фабрика для начальной инициализации приложения.</param>
        /// <param name="controller">Адрес контроллера.</param>
        public TestEnvironment(
            ApplicationFactory applicationFactory,
            string controller)
        {
            _applicationFactory = applicationFactory;
            _controller = controller;
        }

        /// <inheritdoc />
        public HttpClient CreateClient()
        {
            var httpClient = _applicationFactory.CreateClient();

            httpClient.BaseAddress = new Uri(httpClient.BaseAddress, _controller);

            return httpClient;
        }
    }
}