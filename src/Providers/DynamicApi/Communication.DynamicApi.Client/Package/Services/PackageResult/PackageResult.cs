using Communication.ClientProxy;

namespace Communication.DynamicApi.Client
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
