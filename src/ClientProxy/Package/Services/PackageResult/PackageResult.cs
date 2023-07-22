namespace Communication.ClientProxy
{
    internal class PackageResult : IPackageResult
    {
        public PackageResult(IClientProxyProvider proxyProvider)
        {
            ProxyProvider = proxyProvider;

        }
        public IClientProxyProvider ProxyProvider { get; }
    }
}
