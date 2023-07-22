using Microsoft.Extensions.DependencyInjection;

namespace Communication.DynamicApi.Client
{
    public interface IDynamicApiClientCapabilityBuilder
    {
        IDynamicApiClientCapability Build();
    }
    internal class DynamicApiClientCapabilityBuilder : IDynamicApiClientCapabilityBuilder
    {
        public DynamicApiClientCapabilityBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public IDynamicApiClientCapability Build()
        {

            Services.AddSingleton<IDynamicApiClientCapability, DynamicApiClientCapability>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IDynamicApiClientCapability>();
        }
    }
}
