using Net.Core.Communication.ClientProxy;
using Net.Core.Communication.ClientProxy;
using Net.Core.Communication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Net.Core.Communication.Injection.Client
{
    internal class RootServiceProvider
    {
        public RootServiceProvider(IServiceProvider provider)
        {
            this.Provider = provider;
        }
        public IServiceProvider Provider { get; }
    }
    internal class InjectionClientPackageBuilder : IInjectionClientPackageBuilder
    {
        public InjectionClientPackageBuilder(IServiceProvider provider, IEnumerable<ICommunicationFrameDescriptor> frames)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(frames);
            Services.AddSingleton(new RootServiceProvider(provider));
        }

        public IServiceCollection Services { get; }

        public IPackageResult Build()
        {
            Services.AddSingleton<ICommunicationFrameClientFilter, CommunicationFrameClientFilter>();
            Services.AddSingleton<IClientProxyFactory, ClientProxyFactory>();
            Services.AddSingleton<IPackageResult, PackageResult>();


            var provider = Services.BuildServiceProvider();

            return provider.GetRequiredService<IPackageResult>();
        }


    }
}
