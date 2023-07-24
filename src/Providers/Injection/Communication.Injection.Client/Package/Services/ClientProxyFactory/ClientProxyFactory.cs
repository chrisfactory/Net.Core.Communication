using Net.Core.Communication.ClientProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.Core.Communication.Injection.Client
{
    internal class ClientProxyFactory : IClientProxyFactory
    {
        private readonly IReadOnlyDictionary<Type, IInjectionClientFeature> _schemaScope;
        private readonly IServiceProvider _provider;
        public ClientProxyFactory(RootServiceProvider provider, ICommunicationFrameClientFilter filter)
        {
            _provider = provider.Provider;
            var perimeter = filter.GetPerimeter();
            var schemaScope = new Dictionary<Type, IInjectionClientFeature>();
            foreach (var item in perimeter)
            {
                var feature = item.GetFeature<IInjectionClientFeature>();

                if (schemaScope.ContainsKey(feature.ServiceType))
                    throw new InvalidOperationException($"{nameof(ClientProxyFactory)}: Multi definition for communication service: {feature.ServiceType}");
                schemaScope.Add(feature.ServiceType, feature);
            }
            _schemaScope = schemaScope;
        }

        public int PriorityLevel => 0;

        public IClientProxy<TService> Create<TService>()
        {
            return new DisposableProxy<TService>(_provider.GetRequiredService<TService>());
        }

        public bool CanCreate<TService>()
        {
            return _schemaScope.ContainsKey(typeof(TService));
        }

        public IFeature GetFeature<TService>()
        {
            return _schemaScope[typeof(TService)];
        }

        public Type[] GetManagedServices()
        {
            return _schemaScope.Keys.ToArray();
        }



        private class DisposableProxy<TService> : IClientProxy<TService>
        {
            public DisposableProxy(TService instance)
            {
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
                return $"Injection Client: {typeof(TService).FullName}";
            }

        }

    }
}
