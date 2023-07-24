using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication
{
    public interface ICapabilityPackage
    {
        void LoadPackage(IServiceCollection services);
    }
}
