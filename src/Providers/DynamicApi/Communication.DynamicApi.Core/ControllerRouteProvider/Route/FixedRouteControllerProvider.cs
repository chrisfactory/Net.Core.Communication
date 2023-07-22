using Communication.DynamicApi;

namespace Communication
{
    public class FixedRouteControllerProvider : IRouteControllerProvider
    {
        private readonly string _fixedRoute;
        public FixedRouteControllerProvider(string fixedRoute)
        {
            _fixedRoute = fixedRoute;
        }
        public virtual string GetName(ISchemaApi schema)
        {
            return schema.ServiceType.FullName;
        }

        public virtual string GetTemplate(ISchemaApi schema)
        {
            return _fixedRoute;
        }
    }
}
