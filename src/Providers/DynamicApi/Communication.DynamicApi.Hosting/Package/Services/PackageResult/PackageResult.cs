using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Communication.DynamicApi.Hosting
{
    internal class PackageResult : IPackageResult
    {
        public PackageResult(IApplicationModelProvider applicationModelProvider, ISchemaScopeProvider schemaScopeProvider)
        {
            ApplicationModelProvider = applicationModelProvider;
            SchemaScopeProvider = schemaScopeProvider;
        }

        public IApplicationModelProvider ApplicationModelProvider { get; }
        public ISchemaScopeProvider SchemaScopeProvider { get; }

    }
}
