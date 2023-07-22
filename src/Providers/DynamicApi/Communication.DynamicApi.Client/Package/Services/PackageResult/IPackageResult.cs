using Communication.ClientProxy;

namespace Communication.DynamicApi.Client
{
    internal interface IPackageResult
    {
        IClientProxyFactory Factory { get; }
    }
}
