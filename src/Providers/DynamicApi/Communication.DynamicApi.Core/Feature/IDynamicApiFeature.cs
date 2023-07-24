using Net.Core.Communication;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi
{
    public interface IDynamicApiFeature : IFeature
    {
        TypeInfo ServiceType { get; }
        IRouteControllerProvider RouteProvider { get; }
        ISchemaApiProvider SchemaApiProvider { get; }
    }
}
