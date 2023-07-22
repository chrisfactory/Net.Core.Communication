using System.Collections.Generic;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    public interface ISchemaScopeProvider
    {
        IReadOnlyDictionary<TypeInfo, ISchemaApi> Get();
    }
}
