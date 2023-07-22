using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    internal enum MethodTypeCall
    {
        VoidInvoke,
        VoidInvokeRequest,
        TypeInvoke,
        TypeInvokeRequest
    }

    internal class SchemaApiAction : ISchemaApiAction
    {
        private readonly MethodInfo _ProxyMethod;
        private readonly MethodTypeCall _typeCall;
        internal SchemaApiAction(MethodTypeCall typeCall, string actionName, int argCount, MethodInfo serviceMethod, MethodInfo proxyMethod)
        {
            _typeCall = typeCall;
            ServiceMethod = serviceMethod;
            _ProxyMethod = proxyMethod;
            ActionName = actionName;
            ArgCount = argCount;
        }

        public string ActionName { get; }
        public int ArgCount { get; }
        public MethodInfo ServiceMethod { get; }


        public MethodInfo MakeProxyMethod(TypeInfo serviceApiType)
        {
            try
            {
                switch (_typeCall)
                {
                    case MethodTypeCall.VoidInvoke:
                        return _ProxyMethod;
                    case MethodTypeCall.VoidInvokeRequest:
                        { 
                            return _ProxyMethod.MakeGenericMethod(ServiceMethod.GetParameters()[0].ParameterType);

                        }
                    case MethodTypeCall.TypeInvoke:
                        return _ProxyMethod.MakeGenericMethod(ServiceMethod.ReturnType);
                    case MethodTypeCall.TypeInvokeRequest:
                        { 
                            return _ProxyMethod.MakeGenericMethod(ServiceMethod.ReturnType, ServiceMethod.GetParameters()[0].ParameterType);
                        }
                }
            }
            catch (Exception ex)
            {

                throw new SchemaApiException($"{serviceApiType.FullName} -> {ActionName}", ex);
            }

            throw new InvalidOperationException(nameof(MakeProxyMethod));
        }
    }
    public class SchemaApiException : Exception
    {
        public SchemaApiException(string message) : base(message) { }
        public SchemaApiException(string message,Exception ex) : base(message,ex) { }

    }
}
