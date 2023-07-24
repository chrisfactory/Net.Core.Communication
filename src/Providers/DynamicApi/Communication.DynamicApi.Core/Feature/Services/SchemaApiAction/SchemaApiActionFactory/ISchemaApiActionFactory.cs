using System.Reflection;

namespace Net.Core.Communication.DynamicApi
{
    public interface ISchemaApiActionFactory
    {
        ISchemaApiAction CreateAction(MethodInfo serviceMethod, string actionName);
    }
}
