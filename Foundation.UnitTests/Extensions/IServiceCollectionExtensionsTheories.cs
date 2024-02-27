using Foundation.Extensions;
using Foundation.UnitTests.Resources.Fakes;
using Foundation.UnitTests.Resources.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Foundation.UnitTests.Application.Handlers.Queries.QueryHandlerBaseTheories;

namespace Foundation.UnitTests.Extensions
{
    public class IServiceCollectionExtensionsTheories
    {
        [Theory(DisplayName = "IServiceCollectionExtensions AddImplementationsOf Tests")]
        [InlineData(typeof(IFakeInterface<,>), typeof(FakeImplementationOne))]
        [InlineData(typeof(IFakeInterface<,>), typeof(FakeImplementationTwo))]
        public void AddImplementationsOfMethod(Type interfaceType, Type implementationType)
        {
            var services = new ServiceCollection();
            var assembly = Assembly.GetAssembly(interfaceType);

            Action act = () => {
                services.AddImplementationsOf(assembly, interfaceType, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient);
                services.BuildServiceProvider().GetRequiredService(implementationType);
            };

            act.Should().NotThrow();
        }
    }
}