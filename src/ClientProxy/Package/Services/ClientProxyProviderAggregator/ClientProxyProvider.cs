using System;
using System.Collections.Generic;

namespace Net.Core.Communication.ClientProxy
{
    public class FactorySelectorResolver
    {
        private List<IClientProxyFactory> _usableProxy = new List<IClientProxyFactory>();

        public IReadOnlyList<IClientProxyFactory> All() => _usableProxy;


        public IClientProxyFactory Selected { get; private set; }
        internal void Use(IClientProxyFactory factory)
        {
            if (Selected == null || Selected.PriorityLevel > factory.PriorityLevel)
                Selected = factory;

            _usableProxy.Add(factory);
        }

        public override string ToString()
        {
            return Selected?.ToString() ?? base.ToString();
        }
    }

    internal class ClientProxyProvider : IClientProxyProvider
    {
        private Dictionary<Type, FactorySelectorResolver> _factoryIndexer = new Dictionary<Type, FactorySelectorResolver>();
        public ClientProxyProvider(IEnumerable<IClientProxyFactory> factories)
        {
            foreach (var factory in factories)
            {
                foreach (var serviceType in factory.GetManagedServices())
                {
                    if (!_factoryIndexer.ContainsKey(serviceType))
                        _factoryIndexer.Add(serviceType, new FactorySelectorResolver());
                    _factoryIndexer[serviceType].Use(factory);
                }
            }
        }
        public IClientProxy<TServiceProxy> Get<TServiceProxy>()
        {
            return _factoryIndexer[typeof(TServiceProxy)].Selected.Create<TServiceProxy>();
        }

        public IClientProxy<TServiceProxy> Get<TServiceProxy>(Func<IReadOnlyList<IClientProxyFactory>, IClientProxyFactory> select)
        {
            if (select == null) return null;

            var factory = select(_factoryIndexer[typeof(TServiceProxy)].All());
            if (factory == null) return null;

            return factory.Create<TServiceProxy>();
        }


    }
}
