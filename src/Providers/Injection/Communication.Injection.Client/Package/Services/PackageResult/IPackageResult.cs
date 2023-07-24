using Net.Core.Communication.ClientProxy;

namespace Net.Core.Communication.Injection.Client
{
    internal interface IPackageResult
    {
        IClientProxyFactory Factory { get; }
    }
}
