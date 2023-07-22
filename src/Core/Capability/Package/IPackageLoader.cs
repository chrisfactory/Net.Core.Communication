using Microsoft.Extensions.DependencyInjection;

namespace Communication
{
    internal interface IPackageLoader
    {
        void Load(IServiceCollection services);
    }
}
