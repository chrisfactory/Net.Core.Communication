using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic; 
namespace Net.Core.Communication.ClientProxy
{
    internal class ClientProxyPackageBuilder : IClientProxyPackageBuilder
    {
        public ClientProxyPackageBuilder(IEnumerable<IClientProxyFactory> factories)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(factories);
        }

        public IServiceCollection Services { get; }

        public IPackageResult Build()
        { 
            Services.AddSingleton<IClientProxyProvider, ClientProxyProvider>();
            Services.AddSingleton<IPackageResult, PackageResult>();

            var provider = Services.BuildServiceProvider(); 
            return provider.GetRequiredService<IPackageResult>();
        }
    }
}
