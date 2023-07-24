using Net.Core.Communication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Net.Core.Communication.Injection.Client
{
    public class InjectionClientPackage : ICapabilityPackage
    {
        public void LoadPackage(IServiceCollection services)
        {
            services.TryAddSingleton<IInjectionClientPackageBuilder, InjectionClientPackageBuilder>();

            services.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IInjectionClientPackageBuilder>();
                return builder.Build();
            });

            services.AddSingleton(p => p.GetRequiredService<IPackageResult>().Factory);
        }

    }
}
