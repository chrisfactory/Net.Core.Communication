using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    internal interface IDynamicApiHostingPackageBuilder
    {
        IServiceCollection Services { get; }
        IPackageResult Build();
    }
}
