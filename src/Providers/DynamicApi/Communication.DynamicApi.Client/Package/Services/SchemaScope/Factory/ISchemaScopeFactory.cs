using System.Collections.Generic;
using System.Reflection;

namespace Communication.DynamicApi.Client
{
    public interface ISchemaScopeFactory
    {
        IReadOnlyDictionary<TypeInfo, ISchemaApi> Create();
    }
}
