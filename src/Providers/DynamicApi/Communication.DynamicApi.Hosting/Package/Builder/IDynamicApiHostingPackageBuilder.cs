using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Hosting
{
    internal interface IDynamicApiHostingPackageBuilder
    {
        IServiceCollection Services { get; }
        IPackageResult Build();
    }
}
