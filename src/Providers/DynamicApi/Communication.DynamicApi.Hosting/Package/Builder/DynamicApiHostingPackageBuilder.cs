using Communication;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Communication.DynamicApi.Hosting
{
    internal class DynamicApiHostingPackageBuilder : IDynamicApiHostingPackageBuilder
    {
        public DynamicApiHostingPackageBuilder(IUrlHelperFactory urlHelper, IEnumerable<ICommunicationFrameDescriptor> frames)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(frames);
            Services.AddSingleton(urlHelper);
        }

        public IServiceCollection Services { get; }

        public IPackageResult Build()
        {
            Services.AddSingleton<ISchemaScopeFactory, SchemaScopeFactory>();
            Services.AddSingleton<ISchemaScopeProvider, SchemaScopeProvider>();
            Services.AddSingleton<ICommunicationFrameHostingFilter, CommunicationFrameHostingFilter>();
            Services.AddSingleton<IApplicationModelProvider, ApplicationDynamicApiModelProvider>();
            Services.AddSingleton<IPackageResult, PackageResult>();
            

            var provider = Services.BuildServiceProvider();

            return provider.GetRequiredService<IPackageResult>();
        }

      
    }
}
