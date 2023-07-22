using Communication.DynamicApi.Core.Feature.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Client
{
    internal class DynamicApiClientFeatureBuilder : DynamicApiFeatureBuilderBase, IDynamicApiClientFeatureBuilder
    {
        public DynamicApiClientFeatureBuilder(ServiceDescriptor service) : base(service)
        { 
            Services.AddSingleton<ISchemaApiActionFactory, SchemaApiActionFactory>();
            Services.AddSingleton<ISchemaApi, SchemaApi>();
            this.UseRouteDefaul();
        }


        public override IDynamicApiFeature Build()
        {
            Services.AddSingleton<IDynamicApiClientFeature, DynamicApiClientFeature>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IDynamicApiClientFeature>();
        }
    }
}
