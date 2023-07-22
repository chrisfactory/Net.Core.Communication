using Communication.ClientProxy;

namespace Communication.Injection.Client
{
    internal interface IPackageResult
    {
        IClientProxyFactory Factory { get; }
    }
}
