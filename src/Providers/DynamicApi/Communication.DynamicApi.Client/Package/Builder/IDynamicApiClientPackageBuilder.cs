using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.DynamicApi.Client
{
    internal interface IDynamicApiClientPackageBuilder
    {
        IServiceCollection Services { get; }
        IPackageResult Build();
    }
}
