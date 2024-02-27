using Foundation.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddImplementationsOf(this IServiceCollection services, Assembly? assembly, Type type, ServiceLifetime lifetime)
        {
            ArgumentNullException.ThrowIfNull(assembly);

            var rules = assembly.GetTypes().Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == type));

            foreach (var rule in rules)
            {
                services.Add(new ServiceDescriptor(rule, rule, lifetime));
            }

            return services;
        }

        public static IServiceCollection AddImplementationsOf(this IServiceCollection services, Type type, ServiceLifetime lifetime)
        {
            services.AddImplementationsOf(Assembly.GetAssembly(type), type, lifetime);
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddImplementationsOf(typeof(IMessageHandler<>), ServiceLifetime.Transient);
            services.AddImplementationsOf(typeof(IMessageHandler<,>), ServiceLifetime.Transient);

            return services;
        }
    }
}
