using Net.Core.Communication;
using Net.Core.Communication.DynamicApi.Core.Feature.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    internal class DynamicApiHostingFeatureBuilder : DynamicApiFeatureBuilderBase, IDynamicApiHostingFeatureBuilder
    {
        public DynamicApiHostingFeatureBuilder(ServiceDescriptor service) : base(service)
        { 
            Services.AddSingleton<IProxyProvider, HostingProxyProvider>();
            Services.AddSingleton<ISchemaApiActionFactory, SchemaApiActionFactory>();
            Services.AddSingleton<ISchemaApi, SchemaApi>();
        }


        public override IDynamicApiFeature Build()
        {
            Services.AddSingleton<IDynamicApiHostingFeature, DynamicApiHostingFeature>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IDynamicApiHostingFeature>();
        }
    }
}
