using System;
using System.Reflection;

namespace Communication.DynamicApi
{
    public interface IProxyProvider
    {
        TypeInfo GetProxyType(Type serviceType);
    }
}
