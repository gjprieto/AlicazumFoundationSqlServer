using Foundation.Application.Handlers;
using Foundation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Setup
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime)
        {
            services.AddImplementationsOf(assembly, typeof(IMessageHandler<>), lifetime);
            services.AddImplementationsOf(assembly, typeof(IMessageHandler<,>), lifetime);
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services, ServiceLifetime lifetime) 
        {
            services.AddHandlers(Assembly.GetExecutingAssembly(), lifetime);
            return services;
        }
    }
}