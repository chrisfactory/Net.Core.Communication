using Microsoft.Extensions.DependencyInjection;

namespace Communication
{
    public interface ICapabilityPackage
    {
        void LoadPackage(IServiceCollection services);
    }
}
