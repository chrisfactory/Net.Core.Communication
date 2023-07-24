using System.Reflection;

namespace Net.Core.Communication.DynamicApi
{
    public interface ISchemaApiAction
    {
        string ActionName { get; }
        MethodInfo ServiceMethod { get; }
        MethodInfo MakeProxyMethod(TypeInfo serviceApiType);
    }
}
