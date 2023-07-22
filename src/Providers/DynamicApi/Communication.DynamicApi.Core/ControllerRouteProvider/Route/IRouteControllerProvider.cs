using Communication.DynamicApi;

namespace Communication
{
    public interface IRouteControllerProvider
    {
        string GetName(ISchemaApi schema);
        string GetTemplate(ISchemaApi schema);
    }
}
