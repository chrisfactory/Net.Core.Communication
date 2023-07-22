using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Communication;

namespace Communication.DynamicApi.Hosting
{

    internal class SchemaApi : ISchemaApi
    {
        private readonly IReadOnlyDictionary<string, ISchemaApiAction> _byName;
        private readonly IReadOnlyDictionary<MethodInfo, ISchemaApiAction> _byServiceMethodInfo;
        public SchemaApi(IDynamicApiHostingFeature descriptor, IProxyProvider proxyProvider, ISchemaApiActionProvider actionFactory)
        {
            Feature = descriptor;
            ServiceType = descriptor.ServiceType; 
            RouteProvider = descriptor.RouteProvider;
            ProxyType = proxyProvider.GetProxyType(descriptor.ServiceType);

            var bn = new Dictionary<string, ISchemaApiAction>();
            var bmi = new Dictionary<MethodInfo, ISchemaApiAction>();
            foreach (var action in actionFactory.GetActions(ServiceType))
            {
                bn.Add(action.Key, action.Value);
                bmi.Add(action.Value.ServiceMethod, action.Value);
            }
            _byName = bn;
            _byServiceMethodInfo = bmi;
        }

        public ISchemaApiAction this[string key] => _byName[key];
        public ISchemaApiAction this[MethodInfo key] => _byServiceMethodInfo[key];

        public TypeInfo ServiceType { get; }
        public TypeInfo ProxyType { get; }
        public IReadOnlyDictionary<string, ISchemaApiAction> Actions { get; }
        public IRouteControllerProvider RouteProvider { get; }
        public IDynamicApiFeature Feature { get; }

        public IEnumerator<ISchemaApiAction> GetEnumerator()
        {
          return _byName.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _byName.Values.GetEnumerator();
        }
    }
}
