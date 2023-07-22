using Communication.DynamicApi.Client;
using Communication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IDynamicApiHostingFeatureBuilderExtensions
    {
        public static IDynamicApiClientFeatureBuilder UseRouteDefaul(this IDynamicApiClientFeatureBuilder builder) 
        {
            builder.Services.AddSingleton<IRouteControllerProvider, NameTypeRouteControllerProvider>();
            return builder;
        }

        public static IDynamicApiClientFeatureBuilder UseRoute<TRoute>(this IDynamicApiClientFeatureBuilder builder)
            where TRoute : class, IRouteControllerProvider
        {
            builder.Services.AddSingleton<IRouteControllerProvider, TRoute>();
            return builder;
        }

        public static IDynamicApiClientFeatureBuilder UseRoute(this IDynamicApiClientFeatureBuilder builder, string template)
        {
            builder.Services.AddSingleton<IRouteControllerProvider>(new FixedRouteControllerProvider(template));
            return builder;
        }

    }
}
