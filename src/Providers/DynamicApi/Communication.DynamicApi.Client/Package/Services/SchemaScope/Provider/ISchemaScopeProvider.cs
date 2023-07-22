﻿using System.Collections.Generic;
using System.Reflection;

namespace Communication.DynamicApi.Client
{
    public interface ISchemaScopeProvider
    {
        IReadOnlyDictionary<TypeInfo, ISchemaApi> Get();
    }
}
