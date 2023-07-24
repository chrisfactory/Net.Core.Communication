using Net.Core.Communication.ClientProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi.Client
{
    public interface IDynamicApiClientFeature : IDynamicApiFeature
    {
        IUriProvider BaseAddressProvider { get; }
    }

    internal class DynamicApiClientFeature : IDynamicApiClientFeature
    {
        public DynamicApiClientFeature(ServiceDescriptor serviceDescriptor, IUriProvider uriProvider ,IRouteControllerProvider route, ISchemaApiProvider schemaProvider)
        {
            SchemaApiProvider = schemaProvider;
            RouteProvider = route;
            BaseAddressProvider = uriProvider;
            ServiceType = serviceDescriptor.ServiceType.GetTypeInfo();
        }
        public TypeInfo ServiceType { get; }
        public Type Type => typeof(IDynamicApiClientFeature);
        public Type RelatedCapability => typeof(IDynamicApiClientCapability);

        public ISchemaApiProvider SchemaApiProvider { get; }
        public IRouteControllerProvider RouteProvider { get; } 
        public IUriProvider BaseAddressProvider { get; }
    }
}
