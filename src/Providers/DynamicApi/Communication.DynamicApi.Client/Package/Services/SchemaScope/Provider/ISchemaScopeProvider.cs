using System.Collections.Generic;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi.Client
{
    public interface ISchemaScopeProvider
    {
        IReadOnlyDictionary<TypeInfo, ISchemaApi> Get();
    }
}
