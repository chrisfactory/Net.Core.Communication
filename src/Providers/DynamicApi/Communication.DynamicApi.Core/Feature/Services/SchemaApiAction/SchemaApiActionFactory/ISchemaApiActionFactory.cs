using System.Reflection;

namespace Communication.DynamicApi
{
    public interface ISchemaApiActionFactory
    {
        ISchemaApiAction CreateAction(MethodInfo serviceMethod, string actionName);
    }
}
