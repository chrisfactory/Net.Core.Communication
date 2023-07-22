using System.Reflection;

namespace Communication.DynamicApi
{
    public interface IDynamicApiFeature : IFeature
    {
        TypeInfo ServiceType { get; }
        IRouteControllerProvider RouteProvider { get; }
        ISchemaApiProvider SchemaApiProvider { get; }
    }
}
