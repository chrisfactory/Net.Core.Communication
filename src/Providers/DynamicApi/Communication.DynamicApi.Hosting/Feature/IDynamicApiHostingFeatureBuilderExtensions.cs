using Communication;
using Communication.DynamicApi.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IDynamicApiHostingFeatureBuilderExtensions
    {
        public static IDynamicApiHostingFeatureBuilder UseRouteDefault(this IDynamicApiHostingFeatureBuilder builder)
        {
            builder.Services.AddSingleton<IRouteControllerProvider, NameTypeRouteControllerProvider>();
            return builder;
        }
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
