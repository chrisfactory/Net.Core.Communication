using Net.Core.Communication.ClientProxy;

namespace Net.Core.Communication.DynamicApi.Client
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
