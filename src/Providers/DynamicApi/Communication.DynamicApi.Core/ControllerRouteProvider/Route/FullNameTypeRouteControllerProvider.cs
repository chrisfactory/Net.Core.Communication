using Communication.DynamicApi;

namespace Communication
{
    public class FullNameTypeRouteControllerProvider : IRouteControllerProvider
    {
        public virtual string GetName(ISchemaApi schema)
        {
            return schema.ServiceType.FullName;
        }

        public virtual string GetTemplate(ISchemaApi schema)
        {
            return $"/{schema.ServiceType.FullName}";
        }
    }
}
