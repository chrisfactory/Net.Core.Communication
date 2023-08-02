using Microsoft.Extensions.DependencyInjection;
using Net.Core.Communication.ClientProxy;
using System.Collections.Generic;

namespace Net.Core.Communication.DynamicApi.Client
{
    internal class DynamicApiClientPackageBuilder : IDynamicApiClientPackageBuilder
    {
        public DynamicApiClientPackageBuilder(IEnumerable<ICommunicationFrameDescriptor> frames)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(frames);
        }

        public IServiceCollection Services { get; }

        public IPackageResult Build()
        {
            Services.AddSingleton<ISchemaScopeFactory, SchemaScopeFactory>();
            Services.AddSingleton<ISchemaScopeProvider, SchemaScopeProvider>();
            Services.AddSingleton<ICommunicationFrameClientFilter, CommunicationFrameClientFilter>();
            Services.AddSingleton<IClientProxyFactory, ClientProxyFactory>();
            Services.AddSingleton<IPackageResult, PackageResult>();

            var provider = Services.BuildServiceProvider();

            return provider.GetRequiredService<IPackageResult>();
        }


    }

}
