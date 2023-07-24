using Net.Core.Communication.DynamicApi;
using Net.Core.Communication.DynamicApi.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IDynamicApiHostingFeatureBuilderExtensions
    {
        public static IDynamicApiHostingFeatureBuilder UseRoute<TRoute>(this IDynamicApiHostingFeatureBuilder builder)
            where TRoute : class, IRouteControllerProvider
        {
            builder.Services.AddSingleton<IRouteControllerProvider, TRoute>();
            return builder;
        }

        public static IDynamicApiHostingFeatureBuilder UseRoute(this IDynamicApiHostingFeatureBuilder builder, string template)
        {
            builder.Services.AddSingleton<IRouteControllerProvider>(new FixedRouteControllerProvider(template));
            return builder;
        }
    }
}
