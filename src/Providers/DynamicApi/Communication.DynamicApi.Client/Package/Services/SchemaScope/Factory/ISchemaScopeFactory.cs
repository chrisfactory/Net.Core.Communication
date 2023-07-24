using System.Collections.Generic;
using System.Reflection;

namespace Net.Core.Communication.DynamicApi.Client
{
    public interface ISchemaScopeFactory
    {
        IReadOnlyDictionary<TypeInfo, ISchemaApi> Create();
    }
}
