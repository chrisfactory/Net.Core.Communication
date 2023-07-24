using Net.Core.Communication.DynamicApi.Hosting;
using System.Linq;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    internal class SchemaApiActionFactory : ISchemaApiActionFactory
    {
          
        public ISchemaApiAction CreateAction(MethodInfo serviceMethod, string actionName)
        {
            int paramCount = serviceMethod.GetParameters().Count();
            var withParam = paramCount > 0;
            if (serviceMethod.ReturnType == typeof(void))
            {
                if (withParam)
                    return new SchemaApiAction(MethodTypeCall.VoidInvokeRequest, actionName, paramCount, serviceMethod, IProxyTouch.VoidInvokeRequest);
                else
                    return new SchemaApiAction(MethodTypeCall.VoidInvoke, actionName, paramCount, serviceMethod, IProxyTouch.VoidInvoke);
            }
            else
            {
                if (withParam)
                    return new SchemaApiAction(MethodTypeCall.TypeInvokeRequest, actionName, paramCount, serviceMethod, IProxyTouch.TypeInvokeRequest);
                else
                    return new SchemaApiAction(MethodTypeCall.TypeInvoke, actionName, paramCount, serviceMethod, IProxyTouch.TypeInvoke);

            }
        }
    }
}
