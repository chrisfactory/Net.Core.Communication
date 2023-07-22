using Communication.ClientProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Communication.DynamicApi.Client
{
    internal class ClientProxyFactory : IClientProxyFactory
    {
        private readonly IReadOnlyDictionary<TypeInfo, ISchemaApi> _schemaScope;
        public ClientProxyFactory(ISchemaScopeFactory schemaScope)
        {
            _schemaScope = schemaScope.Create();

        }
        public IClientProxy<TService> Create<TService>()
        {
            if (CanCreate<TService>())
            {
                var schema = _schemaScope[typeof(TService).GetTypeInfo()];
                var dispatcher = DispatchProxy.Create<TService, DynamicApiClientProxy>();
                var innerDispatcher = dispatcher as DynamicApiClientProxy;
                if (innerDispatcher != null)
                {
                    innerDispatcher.Attach(schema);
                }
                return new DisposableProxy<TService>(dispatcher);
            }

            return default(IClientProxy<TService>);
        }


        public int PriorityLevel => 1;


        public bool CanCreate<TService>()
        {
            return _schemaScope.ContainsKey(typeof(TService).GetTypeInfo());
        }

        public IFeature GetFeature<TService>()
        {
            return _schemaScope[typeof(TService).GetTypeInfo()].Feature;
        }

        public Type[] GetManagedServices()
        {
            return _schemaScope.Values.Select(s => s.ServiceType).ToArray();
        }
    }
}
