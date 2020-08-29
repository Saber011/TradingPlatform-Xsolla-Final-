using System;

namespace Parma.Gasps.Urp.Tests.IntegrationTests.Application
{
    /// <summary>
    /// Фабрика для начальной инициализации приложения.
    /// </summary>
    public interface IApplicationFactory
    {
        /// <summary>
        /// Создает тестовое окружение.
        /// </summary>
        /// <param name="controllerPath">Адрес контроллера.</param>
        /// <exception cref="ArgumentException">
        ///     Параметр <paramref name="controllerPath"/> равен `NULL` или содержит пустую строку.
        /// </exception>
        /// <example>
        /// <code>
        /// public sealed class SomeControllerTests : IClassFixture&lt;ApplicationFactory&gt;
        /// {
        ///     private readonly ITestEnvironment _environment;
        ///
        ///     public SomeControllerTests(ApplicationFactory factory)
        ///     {
        ///         _environment = factory.CreateEnvironment("/api/some/");
        ///     }
        /// ...
        /// </code>
        /// </example>
        ITestEnvironment CreateEnvironment(string controllerPath);
    }
}