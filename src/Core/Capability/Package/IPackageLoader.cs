using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication
{
    internal interface IPackageLoader
    {
        void Load(IServiceCollection services);
    }
}
