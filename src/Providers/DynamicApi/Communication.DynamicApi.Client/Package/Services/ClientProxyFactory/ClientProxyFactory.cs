using Net.Core.Communication.ClientProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi.Client
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
                return new DisposableProxy<TService>(dispatcher, ProxyToString(typeof(TService), innerDispatcher.Address));
            }

            return default(IClientProxy<TService>);
        }

        private string ProxyToString(Type type, Uri uri)
        {
            return $"DynamicApi Client: {uri} :: ({type.FullName})";
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


        private class DisposableProxy<TService> : IClientProxy<TService>
        {
            private readonly string _toString;
            public DisposableProxy(TService instance, string toString)
            {
                _toString = toString;
                Proxy = instance;
            }
            public TService Proxy { get; }

            public void Dispose()
            {
                if (Proxy is IDisposable disp)
                    disp.Dispose();

            }

            public override string ToString()
            {
                return _toString;
            }

        }
    }
}
