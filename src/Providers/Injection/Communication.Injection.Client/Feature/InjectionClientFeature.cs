using Net.Core.Communication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Net.Core.Communication.Injection.Client
{
    public interface IInjectionClientFeature : IFeature
    {

    }

    internal class InjectionClientFeature : IInjectionClientFeature
    {
        public InjectionClientFeature(ServiceDescriptor serviceDescriptor)
        {
            ServiceType = serviceDescriptor.ServiceType.GetTypeInfo();
        }
        public TypeInfo ServiceType { get; }
        public Type Type => typeof(IInjectionClientFeature);
        public Type RelatedCapability => typeof(IInjectionClientCapability);
    }
}
