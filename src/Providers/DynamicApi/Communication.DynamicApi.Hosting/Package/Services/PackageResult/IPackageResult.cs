using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    internal interface IPackageResult
    {
        IApplicationModelProvider ApplicationModelProvider { get; }
        ISchemaScopeProvider SchemaScopeProvider { get; }
    }
}
