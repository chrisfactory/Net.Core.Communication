using Net.Core.Communication;
using System;

namespace Net.Core.Communication.ClientProxy
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
