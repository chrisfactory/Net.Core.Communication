using Communication.ClientProxy;

namespace Communication.Injection.Client
{
    internal class PackageResult : IPackageResult
    {
        public PackageResult(IClientProxyFactory factory)
        {
            Factory = factory;
        }

        public IClientProxyFactory Factory { get; }
    }
}
