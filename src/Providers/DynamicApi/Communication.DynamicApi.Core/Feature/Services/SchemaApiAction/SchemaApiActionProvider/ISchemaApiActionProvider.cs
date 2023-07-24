using System;
using System.Collections.Generic;

namespace Net.Core.Communication.DynamicApi
{
    public interface ISchemaApiActionProvider
    {
        IReadOnlyDictionary<string, ISchemaApiAction> GetActions(Type serviceType);
    }
}
