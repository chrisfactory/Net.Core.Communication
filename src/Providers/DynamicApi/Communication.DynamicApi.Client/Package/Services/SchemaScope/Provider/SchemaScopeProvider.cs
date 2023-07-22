using System;
using System.Collections.Generic;
using System.Reflection;

namespace Communication.DynamicApi.Client
{
    internal class SchemaScopeProvider : ISchemaScopeProvider
    {
        private readonly ISchemaScopeFactory _factory;
        private readonly Lazy<IReadOnlyDictionary<TypeInfo, ISchemaApi>> _lAcces;
        public SchemaScopeProvider(ISchemaScopeFactory factory)
        {
            _factory = factory;
            _lAcces = new Lazy<IReadOnlyDictionary<TypeInfo, ISchemaApi>>(() => _factory.Create());
        }
        public IReadOnlyDictionary<TypeInfo, ISchemaApi> Get()
        {
            return _lAcces.Value;  
        }
    }
}
