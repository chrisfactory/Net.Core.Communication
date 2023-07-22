using System;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    internal class HostingProxyProvider : IProxyProvider
    {
        public TypeInfo GetProxyType(Type serviceType)
        {
            return typeof(IHostProxyController<>).MakeGenericType(serviceType).GetTypeInfo();
        }
    }
}
