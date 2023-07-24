using Net.Core.Communication;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        public static ICapabilityBuilder AddPackage<TPackage>(this ICapabilityBuilder builder)
           where TPackage : class, ICapabilityPackage
        {
            builder.Services.AddSingleton<ICapabilityPackage, TPackage>();
            return builder;
        }
        public static ICapabilityBuilder AddPackage<TPackage>(this ICapabilityBuilder builder, Func<IServiceProvider, TPackage> implementationFactory)
            where TPackage : class, ICapabilityPackage
        {
            builder.Services.AddSingleton<ICapabilityPackage, TPackage>(implementationFactory);
            return builder;
        }
    }
}
