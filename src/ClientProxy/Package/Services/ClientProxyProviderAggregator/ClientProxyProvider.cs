using System;
using System.Collections.Generic;
using System.Linq;

namespace Communication.ClientProxy
{
    public class FactorySelectorResolver
    {
        public IClientProxyFactory Selected { get; private set; }
        internal void Use(IClientProxyFactory factory)
        {
            if (Selected == null || Selected.PriorityLevel > factory.PriorityLevel)
                Selected = factory;
        }

        public override string ToString()
        {
            return Selected?.ToString() ?? base.ToString();
        }
    }

    internal class ClientProxyProvider : IClientProxyProvider
    {
        private Dictionary<Type, FactorySelectorResolver> _factoryIndexer = new Dictionary<Type, FactorySelectorResolver>();
        public ClientProxyProvider(IEnumerable<IClientProxyFactory> actories)
        {
            foreach (var factory in actories)
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
    }
}
