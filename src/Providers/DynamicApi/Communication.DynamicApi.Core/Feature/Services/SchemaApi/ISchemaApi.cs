using System.Collections.Generic;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi
{
    public interface ISchemaApi : IEnumerable<ISchemaApiAction>
    {
        ISchemaApiAction this[string key] { get; }
        ISchemaApiAction this[MethodInfo key] { get; }

        TypeInfo ServiceType { get; }
        TypeInfo ProxyType { get; }
        IDynamicApiFeature Feature { get; }
        IRouteControllerProvider RouteProvider { get; }
    }
}
