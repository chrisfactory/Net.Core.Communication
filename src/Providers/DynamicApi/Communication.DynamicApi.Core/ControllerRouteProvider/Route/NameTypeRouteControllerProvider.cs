using Communication.DynamicApi;

namespace Communication
{
    public class NameTypeRouteControllerProvider : IRouteControllerProvider
    {
        public virtual string GetName(ISchemaApi schema)
        {
            return schema.ServiceType.FullName;
        }

        public virtual string GetTemplate(ISchemaApi schema)
        {
            return $"/{schema.ServiceType.Name}";
        }
    }
}
