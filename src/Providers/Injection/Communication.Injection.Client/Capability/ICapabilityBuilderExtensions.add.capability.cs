using Communication;
using Communication.Injection.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ICapabilityBuilderExtensions
    {
        public static ICapabilityBuilder AddInjectionClient(this ICapabilityBuilder builder)
        {
            builder.Services.TryAddSingleton<IInjectionClientCapabilityBuilder, InjectionClientCapabilityBuilder>();

            builder.AddPackage<InjectionClientPackage>();
            builder.AddCapability(p => p.GetRequiredService<IInjectionClientCapabilityBuilder>().Build());

            return builder;
        }
    }
}
