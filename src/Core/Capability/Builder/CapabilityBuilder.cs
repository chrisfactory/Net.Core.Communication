using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication
{
    internal class CapabilityBuilder : ICapabilityBuilder
    {
        private readonly IServiceCollection _rootServices;
        public CapabilityBuilder(IServiceCollection rootServices)
        {
            _rootServices = rootServices;
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public ICapabilityDescriptor Build()
        {
            Services.AddSingleton<IPackageLoader, PackageLoader>();
            Services.AddSingleton<ICapabilityDescriptor, CapabilityDescriptor>();

            var provider = Services.BuildServiceProvider();

            var packageLoader = provider.GetRequiredService<IPackageLoader>();
            packageLoader.Load(_rootServices);

            return provider.GetRequiredService<ICapabilityDescriptor>();
        }
    }
}
