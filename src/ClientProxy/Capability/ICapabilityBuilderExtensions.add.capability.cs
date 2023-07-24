using Net.Core.Communication.ClientProxy;
using Net.Core.Communication;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ICapabilityBuilderExtensions
    {
        public static ICapabilityBuilder AddClientProxyProvider(this ICapabilityBuilder builder)
        {
            builder.Services.TryAddSingleton<IClientProxyCapabilityBuilder, ClientProxyCapabilityBuilder>();

            builder.AddPackage<ClientProxyPackage>();
            builder.AddCapability(p => p.GetRequiredService<IClientProxyCapabilityBuilder>().Build());

            return builder;
        }
    }
}
