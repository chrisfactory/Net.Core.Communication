using System.Collections.Generic;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    public interface ISchemaScopeFactory
    {
        IReadOnlyDictionary<TypeInfo, ISchemaApi> Create();
    }
}
