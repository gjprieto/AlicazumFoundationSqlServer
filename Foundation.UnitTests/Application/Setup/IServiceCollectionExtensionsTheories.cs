using Foundation.Application.Setup;
using Foundation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Application.Setup
{
    public class IServiceCollectionExtensionsTheories
    {
        [Theory(DisplayName = "IServiceCollectionExtensions AddHandlers Tests")]
        [InlineData(typeof(FakeMessageNoResultHandler))]
        [InlineData(typeof(FakeCommandNoResultHandler))]
        [InlineData(typeof(FakeQueryHandler))]
        public void AddImplementationsOfMethod(Type handlerType)
        {
            var logger = new Mock<ILogger>();
            var services = new ServiceCollection();
            var assembly = Assembly.GetAssembly(typeof(IServiceCollectionExtensionsTheories));

            Action act = () => {
                services.AddSingleton<ILogger>(logger.Object);
                services.AddHandlers(assembly, ServiceLifetime.Transient);
                services.BuildServiceProvider().GetRequiredService(handlerType);
            };

            act.Should().NotThrow();
        }
    }
}