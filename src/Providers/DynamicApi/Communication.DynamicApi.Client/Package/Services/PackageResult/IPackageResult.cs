using Net.Core.Communication.ClientProxy;

namespace Net.Core.Communication.DynamicApi.Client
{
    internal interface IPackageResult
    {
        IClientProxyFactory Factory { get; }
    }
}
