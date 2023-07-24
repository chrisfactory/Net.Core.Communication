using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.Injection.Client
{
    internal interface IInjectionClientPackageBuilder
    {
        IServiceCollection Services { get; }
        IPackageResult Build();
    }
}
