using System;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi
{
    public interface IProxyProvider
    {
        TypeInfo GetProxyType(Type serviceType);
    }
}
