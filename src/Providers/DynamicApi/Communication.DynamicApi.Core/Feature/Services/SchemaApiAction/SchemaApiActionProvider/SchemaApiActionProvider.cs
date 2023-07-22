using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Communication.DynamicApi
{

    internal class SchemaApiActionProvider : ISchemaApiActionProvider
    {
        private readonly ISchemaApiActionFactory _factory;
        public SchemaApiActionProvider(ISchemaApiActionFactory factory)
        {
            _factory = factory;
        }
         

        public IReadOnlyDictionary<string, ISchemaApiAction> GetActions(Type serviceType)
        {
            Dictionary<string, ISchemaApiAction> actionsResult = new Dictionary<string, ISchemaApiAction>();

            foreach (var methodServiceByName in GetMethods(serviceType).GroupBy(m => m.Name))
            {
                int idx = 0;
                foreach (var serviceMethod in methodServiceByName.OrderBy(m => m.GetParameters().Count()))
                {
                    var actionName = serviceMethod.Name;
                    if (idx > 0)
                        actionName = $"{actionName}{idx}";

                    var targetMethod = _factory.CreateAction(serviceMethod, actionName);

                    actionsResult.Add(actionName, targetMethod);
                    idx++;
                }

            }
            return actionsResult;
        }
         
        private IReadOnlyList<MethodInfo> GetMethods(Type serviceType)
        {
            var types = new List<Type>();
            if (serviceType.IsInterface)
            {
                types = serviceType.GetInterfaces().ToList();
                types.Add(serviceType);
            }
            else
            {
                types.Add(serviceType);
            }

            var methods = new List<MethodInfo>();
            foreach (var type in types)
                foreach (var method in type.GetMethods())
                {
                    if (method.DeclaringType != typeof(object))
                        methods.Add(method);
                }

            methods = methods.Distinct().ToList();
            return methods;
        }
    }
}
