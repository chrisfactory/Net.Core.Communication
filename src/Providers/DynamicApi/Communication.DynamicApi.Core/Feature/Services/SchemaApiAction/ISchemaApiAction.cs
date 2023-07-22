using System.Reflection;

namespace Communication.DynamicApi
{
    public interface ISchemaApiAction
    {
        string ActionName { get; }
        MethodInfo ServiceMethod { get; }
        MethodInfo MakeProxyMethod(TypeInfo serviceApiType);
    }
}
