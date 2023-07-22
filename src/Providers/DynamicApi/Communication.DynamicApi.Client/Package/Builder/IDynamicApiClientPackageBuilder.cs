using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Client
{
    internal interface IDynamicApiClientPackageBuilder
    {
        IServiceCollection Services { get; }
        IPackageResult Build();
    }
}
