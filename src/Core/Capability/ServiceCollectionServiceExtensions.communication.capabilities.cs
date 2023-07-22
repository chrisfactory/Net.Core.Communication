using Communication;
using Microsoft.AspNetCore.Builder;
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




        public static IApplicationBuilder UseCommunication(this IApplicationBuilder app)
        {
            foreach (var postBuildLoader in app.ApplicationServices.GetServices<IPostBuildLoader>())
                postBuildLoader.PostBuild(app);

            return app;
        }
    }
}
