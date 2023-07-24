using Net.Core.Communication;
using Net.Core.Communication.DynamicApi.Hosting; 
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ICapabilityBuilderExtensions
    {
        public static ICapabilityBuilder AddDynamicApiHosting(this ICapabilityBuilder builder)
        {
            builder.Services.TryAddSingleton<IDynamicApiHostingCapabilityBuilder, DynamicApiHostingCapabilityBuilder>();

            builder.AddPackage<DynamicApiHostingPackage>();
            builder.AddCapability(p =>
            {

                var builder = p.GetRequiredService<IDynamicApiHostingCapabilityBuilder>();
                return builder.Build();
            }); 

            return builder;
        }
    }
}
