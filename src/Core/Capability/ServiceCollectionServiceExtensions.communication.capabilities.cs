using Net.Core.Communication;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddCommunication(this IServiceCollection services, Action<ICapabilityBuilder> builder)
        {
            services.AddOptions();

            var b = new CapabilityBuilder(services);
            builder(b);
            services.AddSingleton(b.Build());
            return services;
        }




        public static IServiceProvider UseCommunication(this IServiceProvider provider)
        {
            foreach (var postBuildLoader in provider.GetServices<IPostBuildLoader>())
                postBuildLoader.PostBuild(provider);

            return provider;
        }
    }
}
