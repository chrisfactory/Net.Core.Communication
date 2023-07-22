using Microsoft.Extensions.DependencyInjection;

namespace Communication
{
    public interface ICapabilityBuilder
    {
        IServiceCollection Services { get; }
    }
}
