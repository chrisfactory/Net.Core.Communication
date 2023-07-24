using Net.Core.Communication;
using System;
using System.Reflection;

namespace Net.Core.Communication.ClientProxy
{
    public interface IClientProxyFeature : IFeature
    { 
    }
    internal class ClientProxyFeature : IClientProxyFeature
    { 
        public Type Type => typeof(IClientProxyFeature);
        public Type RelatedCapability => typeof(IClientProxyCapability); 
        public TypeInfo ServiceType => typeof(IClientProxyFactory).GetTypeInfo();
    }
}
