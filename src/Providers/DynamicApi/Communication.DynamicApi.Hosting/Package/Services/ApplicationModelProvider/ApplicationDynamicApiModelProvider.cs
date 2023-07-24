using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Collections.Generic;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    internal class ApplicationDynamicApiModelProvider : IApplicationModelProvider
    {
        private readonly ISchemaScopeProvider _schemaScopeProvider;
        public ApplicationDynamicApiModelProvider(/*IUrlHelperFactory urlHelper,*/ ISchemaScopeProvider schemaScopeProvider)
        {
            _schemaScopeProvider = schemaScopeProvider;
        }

        int IApplicationModelProvider.Order => -1000 + 10;


        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {

            var schemas = _schemaScopeProvider.Get();

            foreach (var toApi in schemas.Values)
            {
                var typeInfo = toApi.ServiceType;
                var controllerApi = CreateControllerModel(context, toApi);

                foreach (var actionSchema in toApi)
                {
                    var callApiMethod = actionSchema.MakeProxyMethod(typeInfo);
                    var actionName = actionSchema.ActionName;

                    var methodAttribute = new HttpPostAttribute();
                    var route = new RouteAttribute(actionName);
                    var action = new ActionModel(callApiMethod, new object[] { methodAttribute, route })
                    {
                        ActionName = actionName,
                        Controller = controllerApi
                    };

                    foreach (var parameter in callApiMethod.GetParameters())
                    {
                        var prm = new ParameterModel(parameter, new List<object>() { new FromBodyAttribute() { EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Disallow } });
                        prm.ParameterName = parameter.Name;
                       
                        action.Parameters.Add(prm);
                    }


                    var selector = new SelectorModel();
                    selector.ActionConstraints.Add(new HttpMethodActionConstraint(methodAttribute.HttpMethods));
                    selector.AttributeRouteModel = new AttributeRouteModel(route);
                    action.Selectors.Add(selector);
                    controllerApi.Actions.Add(action);
                }



                context.Result.Controllers.Add(controllerApi);
            }
        }

        private static ControllerModel CreateControllerModel(ApplicationModelProviderContext context, ISchemaApi api)
        {
            var proxy = api.ProxyType;

            string template = api.RouteProvider.GetTemplate(api);
            string name = api.RouteProvider.GetName(api);

            var routeApi = new RouteAttribute(template);
            var apiAttributes = new List<object>() { new ApiControllerAttribute(), routeApi };
            var controllerApi = new ControllerModel(proxy, apiAttributes);
            controllerApi.ControllerName = name;
            controllerApi.Application = context.Result;
            controllerApi.ApiExplorer.GroupName = name;

            var apiSelector = new SelectorModel();
            apiSelector.AttributeRouteModel = new AttributeRouteModel(routeApi);
            foreach (var attr in apiAttributes)
                apiSelector.EndpointMetadata.Add(attr);
            controllerApi.Selectors.Add(apiSelector);
            return controllerApi;
        }

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {

        }
    }
}
