using Communication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Communication.DynamicApi.Hosting
{
    public class DynamicApiHostingPackage : ICapabilityPackage
    {
        public void LoadPackage(IServiceCollection services)
        {
            services.TryAddSingleton<IDynamicApiHostingPackageBuilder, DynamicApiHostingPackageBuilder>();

            services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IDynamicApiHostingPackageBuilder>();
                return builder.Build();
            });

            services.AddHttpContextAccessor();
            services.AddSingleton(p => p.GetRequiredService<IPackageResult>().SchemaScopeProvider);
            services.AddSingleton(p => p.GetRequiredService<IPackageResult>().ApplicationModelProvider);
         
            services.AddSingleton(typeof(IHostProxyController<>), typeof(HostProxyController<>));

        }
    }
}
