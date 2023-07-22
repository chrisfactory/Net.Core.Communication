using Microsoft.Extensions.DependencyInjection;

namespace Communication.Injection.Client
{
    internal interface IInjectionClientPackageBuilder
    {
        IServiceCollection Services { get; }
        IPackageResult Build();
    }
}
