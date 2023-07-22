using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    [AuthorizeAttribute]
    internal class HostProxyController<T> : IHostProxyController<T>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _Provider;
        private readonly ISchemaApi _schemaApi;
        public HostProxyController(
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider,
            ISchemaScopeProvider schemaScopeProvider
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _Provider = serviceProvider;
            _schemaApi = schemaScopeProvider.Get()[typeof(T).GetTypeInfo()];

            Throw();
        }

        private void Throw()
        {
            if (_schemaApi.ProxyType != typeof(IHostProxyController<T>).GetTypeInfo())
                throw new InvalidOperationException();

            _ = _Provider.GetRequiredService<T>();
        }

        public void Invoke()
        {
            var context = _httpContextAccessor.HttpContext;
            var path = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

            _schemaApi[path].ServiceMethod.Invoke(_Provider.GetRequiredService<T>(), null);
        }
        public void Invoke<TRequest>(TRequest request)
        {
            var context = _httpContextAccessor.HttpContext;
            var path = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

            _schemaApi[path].ServiceMethod.Invoke(_Provider.GetRequiredService<T>(), new object[] { request });
        }
        public TResult Invoke<TResult>()
        {
            var context = _httpContextAccessor.HttpContext;
            var path = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

            return (TResult)_schemaApi[path].ServiceMethod.Invoke(_Provider.GetRequiredService<T>(), null);
        }

        public TResult Invoke<TResult, TRequest>(TRequest request) 
        {

            var context = _httpContextAccessor.HttpContext;
            var path = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

            return (TResult)_schemaApi[path].ServiceMethod.Invoke(_Provider.GetRequiredService<T>(), new object[] { request });
        }
    }
}
