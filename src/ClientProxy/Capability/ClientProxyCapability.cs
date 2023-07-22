using Communication;
using System;

namespace Communication.ClientProxy
{
    public interface IClientProxyCapability : ICapability
    {

    }
    internal class ClientProxyCapability : IClientProxyCapability
    { 
        public Type Type => typeof(IClientProxyCapability);
        public Type[] RelatedFeatures => new[] { typeof(IClientProxyFeature) };

        public string Name => Type.FullName;
        public string[] Groups => new[] { "client.proxy"};
    }
}
