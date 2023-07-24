using System.Collections.Generic;
using System.Reflection; 

namespace Net.Core.Communication.DynamicApi
{
    //internal class SchemaApi<TFeature> : ISchemaApi
    //    where TFeature : class, IDynamicApiFeature
    //{
    //    private readonly IReadOnlyDictionary<string, ISchemaApiAction> _byName;
    //    private readonly IReadOnlyDictionary<MethodInfo, ISchemaApiAction> _byServiceMethodInfo;
    //    public SchemaApi(TFeature descriptor, IProxyProvider proxyProvider, ISchemaApiActionProvider actionFactory)
    //    {
    //        RouteProvider = descriptor.RouteProvider;
    //        ServiceType = descriptor.ServiceType;

    //        ProxyType = proxyProvider.GetProxyType(descriptor.ServiceType);

    //        var bn = new Dictionary<string, ISchemaApiAction>();
    //        var bmi = new Dictionary<MethodInfo, ISchemaApiAction>();
    //        foreach (var action in actionFactory.GetActions(ServiceType))
    //        {
    //            bn.Add(action.Key, action.Value);
    //            bmi.Add(action.Value.ServiceMethod, action.Value);
    //        }
    //        _byName = bn;
    //        _byServiceMethodInfo = bmi;
    //    }

    //    public ISchemaApiAction this[string key] => _byName[key];
    //    public ISchemaApiAction this[MethodInfo key] => _byServiceMethodInfo[key];

    //    public TypeInfo ServiceType { get; }
    //    public TypeInfo ProxyType { get; }
    //    public IDynamicApiFeature Feature { get; } 
    //    public IRouteControllerProvider RouteProvider { get; }
    //}
}
