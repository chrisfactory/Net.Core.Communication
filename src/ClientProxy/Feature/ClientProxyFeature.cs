using Communication;
using System;
using System.Reflection;

namespace Communication.ClientProxy
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
