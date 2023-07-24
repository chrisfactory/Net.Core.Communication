namespace Net.Core.Communication.DynamicApi
{
    public interface IRouteControllerProvider
    {
        string GetName(ISchemaApi schema);
        string GetTemplate(ISchemaApi schema);
    }
}
