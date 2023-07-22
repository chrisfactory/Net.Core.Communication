using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Communication.DynamicApi.Hosting
{
    internal interface IPackageResult
    {
        IApplicationModelProvider ApplicationModelProvider { get; }
        ISchemaScopeProvider SchemaScopeProvider { get; }
    }
}
