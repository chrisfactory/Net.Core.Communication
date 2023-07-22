using Communication.DynamicApi.Core.Feature.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Hosting
{
    internal class DynamicApiHostingFeatureBuilder : DynamicApiFeatureBuilderBase, IDynamicApiHostingFeatureBuilder
    {
        public DynamicApiHostingFeatureBuilder(ServiceDescriptor service, ICapabilityDescriptor capabilities) : base(service)
        {
            Services.AddSingleton(p => capabilities.Get<IDynamicApiHostingCapability>());
            Services.AddSingleton<IProxyProvider, HostingProxyProvider>();
            Services.AddSingleton<ISchemaApiActionFactory, SchemaApiActionFactory>();
            Services.AddSingleton<ISchemaApi, SchemaApi>();
            this.UseRouteDefault();
        }


        public override IDynamicApiFeature Build()
        {
            Services.AddSingleton<IDynamicApiHostingFeature, DynamicApiHostingFeature>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IDynamicApiHostingFeature>();
        }
    }
}
