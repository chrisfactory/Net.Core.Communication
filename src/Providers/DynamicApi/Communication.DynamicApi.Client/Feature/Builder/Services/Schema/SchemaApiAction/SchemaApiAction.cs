using System;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi.Client
{

    internal class SchemaApiAction : ISchemaApiAction
    {
        internal SchemaApiAction(string actionName, MethodInfo serviceMethod)
        {
            ServiceMethod = serviceMethod;
            ActionName = actionName;
        }

        public string ActionName { get; }
        public MethodInfo ServiceMethod { get; }

        public MethodInfo MakeProxyMethod(TypeInfo serviceApiType)
        {
            throw new NotImplementedException();
        }
    }
}
