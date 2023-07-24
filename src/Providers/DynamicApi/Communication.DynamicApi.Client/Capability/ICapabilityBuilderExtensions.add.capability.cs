using Net.Core.Communication;
using Net.Core.Communication.DynamicApi.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ICapabilityBuilderExtensions
    {
        public static ICapabilityBuilder AddDynamicApiClient(this ICapabilityBuilder builder)
        {
            builder.Services.TryAddSingleton<IDynamicApiClientCapabilityBuilder, DynamicApiClientCapabilityBuilder>();

            builder.AddPackage<DynamicApiClientPackage>();
            builder.AddCapability(p => p.GetRequiredService<IDynamicApiClientCapabilityBuilder>().Build());

            return builder;
        }
    }
}
