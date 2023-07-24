using Net.Core.Communication;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        
        public static ICapabilityBuilder AddCapability<TCapability>(this ICapabilityBuilder builder)
            where TCapability : class, ICapability
        {
            builder.Services.AddSingleton<ICapability, TCapability>();
            return builder;
        }
        public static ICapabilityBuilder AddCapability<TCapability>(this ICapabilityBuilder builder, Func<IServiceProvider, TCapability> implementationFactory)
        where TCapability : class, ICapability
        {
            builder.Services.AddSingleton<ICapability, TCapability>(implementationFactory);
            return builder;
        }
       

    }
}
