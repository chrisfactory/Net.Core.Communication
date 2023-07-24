using Microsoft.Extensions.DependencyInjection;
using System;

namespace Net.Core.Communication
{
    internal class CommunicationBuilder : ICommunicationBuilder
    {
        public CommunicationBuilder(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            Services = new ServiceCollection();
            ServiceDescriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
        }

        public CommunicationBuilder(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            Services = new ServiceCollection();
            ServiceDescriptor = new ServiceDescriptor(serviceType, implementationFactory, lifetime);
        }

        public ServiceDescriptor ServiceDescriptor { get; }
        public IServiceCollection Services { get; }

        public ICommunicationFrameDescriptor Build(IServiceProvider rootProvider)
        {
            Services.AddSingleton(ServiceDescriptor);
            var capabilities = rootProvider.GetRequiredService<ICapabilityDescriptor>();
            Services.AddSingleton(capabilities);

            Services.AddSingleton<IUsedScopeResolver, UsedScopeResolver>();
            Services.AddSingleton(p => p.GetRequiredService<IUsedScopeResolver>().Resolve());
            Services.AddSingleton<ICommunicationFrameDescriptor, CommunicationFrameDescriptor>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<ICommunicationFrameDescriptor>();
        }
    }
}
