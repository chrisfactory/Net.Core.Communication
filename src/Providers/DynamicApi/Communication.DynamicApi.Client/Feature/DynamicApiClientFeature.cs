using Communication.ClientProxy;
using Communication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Communication.DynamicApi.Client
{
    public interface IDynamicApiClientFeature : IDynamicApiFeature
    {
        IUriProvider BaseAdressProvider { get; }
    }

    internal class DynamicApiClientFeature : IDynamicApiClientFeature
    {
        public DynamicApiClientFeature(ServiceDescriptor serviceDescriptor, IUriProvider uriProvider ,IRouteControllerProvider route, ISchemaApiProvider schemaProvider)
        {
            SchemaApiProvider = schemaProvider;
            RouteProvider = route;
            BaseAdressProvider = uriProvider;
            ServiceType = serviceDescriptor.ServiceType.GetTypeInfo();
        }
        public TypeInfo ServiceType { get; }
        public Type Type => typeof(IDynamicApiClientFeature);
        public Type RelatedCapability => typeof(IDynamicApiClientCapability);

        public ISchemaApiProvider SchemaApiProvider { get; }
        public IRouteControllerProvider RouteProvider { get; } 
        public IUriProvider BaseAdressProvider { get; }
    }
}
