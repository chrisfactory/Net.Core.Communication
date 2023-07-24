using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    public interface IDynamicApiHostingCapabilityBuilder
    {
        IDynamicApiHostingCapability Build();
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
