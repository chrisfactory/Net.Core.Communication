using Communication;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Core.Feature.Builder
{
    public interface IDynamicApiFeatureBuilderBase
    {
        IServiceCollection Services { get; }
        IDynamicApiFeature Build();
    }
    public abstract class DynamicApiFeatureBuilderBase : IDynamicApiFeatureBuilderBase
    {
        public DynamicApiFeatureBuilderBase(ServiceDescriptor service)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(service);
            Services.AddSingleton<ISchemaApiFactory, SchemaApiFactory>();
            Services.AddSingleton<ISchemaApiProvider, SchemaApiProvider>();
            Services.AddSingleton<ISchemaApiActionProvider, SchemaApiActionProvider>();


            Services.AddSingleton<IRouteControllerProvider, FullNameTypeRouteControllerProvider>();
        }
        public IServiceCollection Services { get; }
        public abstract IDynamicApiFeature Build();
    }
}
