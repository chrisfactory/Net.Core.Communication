using Communication;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        public static ICommunicationBuilder AddCommunicationServiceTransient<TService>(this IServiceCollection services)
              where TService : class
        { 
            var builder = new CommunicationBuilder(typeof(TService), typeof(TService), ServiceLifetime.Transient);
            services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }

        public static ICommunicationBuilder AddCommunicationServiceTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> provider)
            where TService : class
        {  
            var builder = new CommunicationBuilder(typeof(TService), provider, ServiceLifetime.Transient);
            services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }


        public static ICommunicationBuilder AddCommunicationServiceTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        { 
            var builder = new CommunicationBuilder(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
            services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }


        public static ICommunicationBuilder AddCommunicationServiceTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> provider)
            where TService : class
            where TImplementation : class, TService
        { 
            var builder = new CommunicationBuilder(typeof(TService), provider, ServiceLifetime.Transient);
            services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }

    }
}
