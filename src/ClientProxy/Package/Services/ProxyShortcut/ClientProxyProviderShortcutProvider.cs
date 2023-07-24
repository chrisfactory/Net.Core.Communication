using System;
using System.Collections.Generic;

namespace Net.Core.Communication.ClientProxy
{

    public interface IClientProxyProvider<TService>
    {
        IClientProxy<TService> Get();
        IClientProxy<TService> Get(Func<IReadOnlyList<IClientProxyFactory>, IClientProxyFactory> select);
    }
    internal class ClientProxyProviderShortcutProvider<TService> : IClientProxyProvider<TService>
    {
        private readonly IClientProxyProvider _provider;
        public ClientProxyProviderShortcutProvider(IClientProxyProvider proxyProvider)
        {
            _provider = proxyProvider;
        }
        public IClientProxy<TService> Get()
        {
            return _provider.Get<TService>();
        }
        public IClientProxy<TService> Get(Func<IReadOnlyList<IClientProxyFactory>, IClientProxyFactory> select)
        {
            return _provider.Get<TService>(select); 
        }
         
    }

    public class ClientProxyShortcut<TService> : IClientProxy<TService>
    {
        private readonly IClientProxy<TService> _innerProxy;
        public ClientProxyShortcut(IClientProxyProvider<TService> provider)
        {
            _innerProxy = provider.Get();
        }


        public TService Proxy => _innerProxy.Proxy;

        public void Dispose()
        {
            _innerProxy?.Dispose();
        }

        public override string ToString()
        {
            return _innerProxy.ToString();
        }
    }
}
