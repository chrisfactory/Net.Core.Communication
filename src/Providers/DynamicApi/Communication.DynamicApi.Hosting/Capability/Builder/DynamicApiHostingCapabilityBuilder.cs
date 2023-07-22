using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Hosting
{
    public interface IDynamicApiHostingCapabilityBuilder
    {
        IDynamicApiHostingCapability Build();
        IServiceCollection Services { get; }
    }
    internal class DynamicApiHostingCapabilityBuilder : IDynamicApiHostingCapabilityBuilder
    {
        public DynamicApiHostingCapabilityBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public IDynamicApiHostingCapability Build()
        {

            Services.AddSingleton<IDynamicApiHostingCapability, DynamicApiHostingCapability>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IDynamicApiHostingCapability>();
        }
    }
}
