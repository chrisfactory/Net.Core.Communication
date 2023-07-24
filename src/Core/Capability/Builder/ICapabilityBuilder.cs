using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication
{
    public interface ICapabilityBuilder
    {
        IServiceCollection Services { get; }
    }
}
