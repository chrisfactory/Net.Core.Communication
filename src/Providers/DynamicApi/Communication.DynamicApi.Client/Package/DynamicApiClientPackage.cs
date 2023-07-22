using Communication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Communication.DynamicApi.Client
{
    public class DynamicApiClientPackage : ICapabilityPackage
    {
        public void LoadPackage(IServiceCollection services)
        {
            services.TryAddSingleton<IDynamicApiClientPackageBuilder, DynamicApiClientPackageBuilder>();

            services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IDynamicApiClientPackageBuilder>();
                return builder.Build();
            });

            services.AddSingleton(p => p.GetRequiredService<IPackageResult>().Factory);

        }

     
    }
}
