using Parma.Gasps.Urp.Tests.IntegrationTests.Application;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TradingPlatform.Common.Users.Dto;
using Xbehave;
using Xunit;
using TradingPlatform.Infrastructure;
using FluentAssertions;
namespace TradingPlatformTest1
{
    public class UnitTest1 : IClassFixture<ApplicationFactory>
    {
        private readonly ITestEnvironment _environment;

        public UnitTest1(ApplicationFactory factory)
        {
            _environment = factory.CreateEnvironment("/api/User/");
        }

        [Scenario(DisplayName = "Получение списка прокуратур.")]
        public void GetUsers(
            HttpClient client,
            UsersDto[] users)
        {
            // Arrange
            "Создание `HTTP`-клиента c авторизацией"
                .x(() =>
                {
                    client = _environment
                        .CreateClient();
                });

            // Act
            "Обращение к методу `API` - ` User`"
                .x(async () =>
                {
                    using var response = await client.GetAsync(
                        "User");

                    users = (await response.Content.ReadAsAsync<ServiceResponse<List<UsersDto>>>()).Content.ToArray();
                });

            // Assert
            "Возвращаемый результат содержит элементы"
                .x(() => users.Should().NotBeEmpty());

            "Все идентификаторы прокуратур в результате корректны и уникальны"
                .x(() =>
                {
                    users
                        .Should()
                        .OnlyHaveUniqueItems(id => id.Email);
                });
        }
    }
}
