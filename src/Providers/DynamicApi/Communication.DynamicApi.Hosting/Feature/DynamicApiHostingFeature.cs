using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    public interface IDynamicApiHostingFeature : IDynamicApiFeature
    {

    }

    internal class DynamicApiHostingFeature : IDynamicApiHostingFeature
    {
        public DynamicApiHostingFeature(ServiceDescriptor serviceDescriptor, IRouteControllerProvider route, ISchemaApiProvider schemaProvider)
        {
            SchemaApiProvider = schemaProvider;
            RouteProvider = route;
            ServiceType = serviceDescriptor.ServiceType.GetTypeInfo();
        }
        public TypeInfo ServiceType { get; }
        public Type Type => typeof(IDynamicApiHostingFeature);
        public Type RelatedCapability => typeof(IDynamicApiHostingCapability);


        public ISchemaApiProvider SchemaApiProvider { get; }
        public IRouteControllerProvider RouteProvider { get; }
    }
}
