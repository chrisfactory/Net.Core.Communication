using Microsoft.Extensions.DependencyInjection;
using System;

namespace Communication.DynamicApi
{
    internal class SchemaApiFactory : ISchemaApiFactory
    {
        private readonly IServiceProvider _provider;
        public SchemaApiFactory(IServiceProvider provider)
        {
            _provider = provider;
        }
        public ISchemaApi Create()
        {
            return _provider.GetRequiredService<ISchemaApi>();
        }
    }
}
