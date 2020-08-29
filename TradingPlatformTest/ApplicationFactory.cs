using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Parma.Gasps.Urp.Tests.IntegrationTests.Application
{
    /// <inheritdoc cref='IApplicationFactory'/>
    public sealed class ApplicationFactory : WebApplicationFactory<Startup>, IApplicationFactory
    {
        /// <inheritdoc />
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseSolutionRelativeContentRoot(string.Empty)
                .UseStartup<Startup>();
        }

        /// <inheritdoc />
        public ITestEnvironment CreateEnvironment(string controllerPath)
        {
            Guard.NotNullOrWhiteSpace(controllerPath, nameof(controllerPath));

            var path = new StringBuilder()
                .Append(controllerPath.TrimEnd('/'))
                .Append('/')
                .ToString();

            return new TestEnvironment(this, path);
        }
    }
}