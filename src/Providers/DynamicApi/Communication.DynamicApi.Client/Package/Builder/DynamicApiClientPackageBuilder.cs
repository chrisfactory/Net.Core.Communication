using Net.Core.Communication.ClientProxy;
using Net.Core.Communication;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Net.Core.Communication.DynamicApi.Client.DynamicApiClientPackage;

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
