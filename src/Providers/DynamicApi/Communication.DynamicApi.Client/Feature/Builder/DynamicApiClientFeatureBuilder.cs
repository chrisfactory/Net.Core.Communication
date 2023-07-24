using Net.Core.Communication.DynamicApi.Core.Feature.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.DynamicApi.Client
{
    internal class DynamicApiClientFeatureBuilder : DynamicApiFeatureBuilderBase, IDynamicApiClientFeatureBuilder
    {
        public DynamicApiClientFeatureBuilder(ServiceDescriptor service) : base(service)
        { 
            Services.AddSingleton<ISchemaApiActionFactory, SchemaApiActionFactory>();
            Services.AddSingleton<ISchemaApi, SchemaApi>();
        }


        public override IDynamicApiFeature Build()
        {
            Services.AddSingleton<IDynamicApiClientFeature, DynamicApiClientFeature>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IDynamicApiClientFeature>();
        }
    }
}
