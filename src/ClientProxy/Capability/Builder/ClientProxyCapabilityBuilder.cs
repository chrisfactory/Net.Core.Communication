using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.ClientProxy
{
    public interface IClientProxyCapabilityBuilder
    {
        IClientProxyCapability Build();
    }
    internal class ClientProxyCapabilityBuilder : IClientProxyCapabilityBuilder
    {
        public ClientProxyCapabilityBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public IClientProxyCapability Build()
        {

            Services.AddSingleton<IClientProxyCapability, ClientProxyCapability>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IClientProxyCapability>();
        }
    }
}
