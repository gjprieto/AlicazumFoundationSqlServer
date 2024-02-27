using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Resources.Mocks
{
    public static class ServiceDescriptionExtensions
    {
        public static bool Is<TService, TInstance>(this ServiceDescriptor serviceDescriptor, ServiceLifetime lifetime)
        {
            return serviceDescriptor.ServiceType == typeof(TService) && serviceDescriptor.ImplementationType == typeof(TInstance) && serviceDescriptor.Lifetime == lifetime;
        }
    }

    public sealed class ServiceCollectionVerifier(Mock<IServiceCollection> serviceCollectionMock)
    {
        private readonly Mock<IServiceCollection> _serviceCollectionMock = serviceCollectionMock;

        public void ContainsSingletonService<TService, TInstance>()
        {
            this.IsRegistered<TService, TInstance>(ServiceLifetime.Singleton);
        }

        public void ContainsTransientService<TService, TInstance>()
        {
            this.IsRegistered<TService, TInstance>(ServiceLifetime.Transient);
        }

        public void ContainsScopedService<TService, TInstance>()
        {
            this.IsRegistered<TService, TInstance>(ServiceLifetime.Scoped);
        }

        private void IsRegistered<TService, TInstance>(ServiceLifetime lifetime)
        {
            this._serviceCollectionMock
                .Verify(serviceCollection => serviceCollection.Add(It.Is<ServiceDescriptor>(serviceDescriptor => serviceDescriptor.Is<TService, TInstance>(lifetime))));

        }
    }

    public sealed class ServiceCollectionMock
    {
        private readonly Mock<IServiceCollection> serviceCollectionMock;

        public ServiceCollectionMock()
        {
            this.serviceCollectionMock = new Mock<IServiceCollection>();
            this.ServiceCollectionVerifier = new ServiceCollectionVerifier(this.serviceCollectionMock);
        }

        public IServiceCollection ServiceCollection => this.serviceCollectionMock.Object;

        public ServiceCollectionVerifier ServiceCollectionVerifier { get; set; }

        public void ContainsSingletonService<TService, TInstance>()
        {
            this.ServiceCollectionVerifier.ContainsSingletonService<TService, TInstance>();
        }

        public void ContainsTransientService<TService, TInstance>()
        {
            this.ServiceCollectionVerifier.ContainsTransientService<TService, TInstance>();
        }

        public void ContainsScopedService<TService, TInstance>()
        {
            this.ServiceCollectionVerifier.ContainsScopedService<TService, TInstance>();
        }
    }
}
