namespace Communication.ClientProxy
{

    public interface IClientProxyProvider<TService>
    {
        IClientProxy<TService> Get();
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
    }

    public class ClientProxyShortcut<TService> : IClientProxy<TService>
    {
        private readonly IClientProxy<TService>  _innerProxy;
        public ClientProxyShortcut(IClientProxyProvider<TService> provider)
        {
            _innerProxy = provider.Get(); 
        }
         

        public TService Proxy => _innerProxy.Proxy;

        public void Dispose()
        {
            _innerProxy?.Dispose();
        }
    }
}
