using Net.Core.Communication.DynamicApi;
using Net.Core.Communication.DynamicApi.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IDynamicApiHostingFeatureBuilderExtensions
    {
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
