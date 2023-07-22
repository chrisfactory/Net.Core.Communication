using System;
using System.Collections.Generic;

namespace Communication.DynamicApi
{
    public interface ISchemaApiActionProvider
    {
        IReadOnlyDictionary<string, ISchemaApiAction> GetActions(Type serviceType);
    }
}
