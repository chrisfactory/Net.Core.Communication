using Microsoft.Extensions.DependencyInjection;

namespace Communication.Injection.Client
{
    public interface IInjectionClientCapabilityBuilder
    {
        IInjectionClientCapability Build();
    }
    internal class InjectionClientCapabilityBuilder : IInjectionClientCapabilityBuilder
    {
        public InjectionClientCapabilityBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public IInjectionClientCapability Build()
        {

            Services.AddSingleton<IInjectionClientCapability, InjectionClientCapability>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IInjectionClientCapability>();
        }
    }
}
