using Communication;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        public static ICommunicationBuilder AddCommunicationServiceSingleton<TService>(this IServiceCollection services)
              where TService : class
        {
            var builder = new CommunicationBuilder(typeof(TService), typeof(TService), ServiceLifetime.Singleton);
            //services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }

        public static ICommunicationBuilder AddCommunicationServiceSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> provider)
            where TService : class
        {
            var builder = new CommunicationBuilder(typeof(TService), provider, ServiceLifetime.Singleton);
           // services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }


        public static ICommunicationBuilder AddCommunicationServiceSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        { 
            var builder = new CommunicationBuilder(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);
            services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }


        public static ICommunicationBuilder AddCommunicationServiceSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> provider)
            where TService : class
            where TImplementation : class, TService
        { 
            var builder = new CommunicationBuilder(typeof(TService), provider, ServiceLifetime.Singleton);
            services.Add(builder.ServiceDescriptor);
            services.AddSingleton(p => builder.Build(p));
            return builder;
        }

    }
}
