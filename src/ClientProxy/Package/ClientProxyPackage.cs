using Communication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Communication.ClientProxy
{
    public class ClientProxyPackage : ICapabilityPackage
    {
        public void LoadPackage(IServiceCollection services)
        {
            services.TryAddSingleton<IClientProxyPackageBuilder, ClientProxyPackageBuilder>();

            services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IClientProxyPackageBuilder>();
                return builder.Build();
            });

            services.AddSingleton(p => p.GetRequiredService<IPackageResult>().ProxyProvider);
            //disposable transient:
            services.AddTransient(typeof(IClientProxyProvider<>), typeof(ClientProxyProviderShortcutProvider<>));
            services.AddTransient(typeof(IClientProxy<>),typeof(ClientProxyShortcut<>));
         
        }
    }
}
