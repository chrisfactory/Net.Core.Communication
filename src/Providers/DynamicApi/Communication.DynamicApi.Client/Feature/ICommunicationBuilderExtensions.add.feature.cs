using Net.Core.Communication.ClientProxy;
using Net.Core.Communication;
using Net.Core.Communication.DynamicApi;
using Net.Core.Communication.DynamicApi.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ICommunicationBuilderExtensions
    {
        public static ICommunicationBuilder WithDynamicApiClient(this ICommunicationBuilder builder, Action<IDynamicApiClientFeatureBuilder> feature = null)
        {
            builder.Services.TryAddSingleton<IDynamicApiClientFeatureBuilder, DynamicApiClientFeatureBuilder>();

            builder.AddFeature(p =>
            {
                var featureBuilder = p.GetRequiredService<IDynamicApiClientFeatureBuilder>(); 
                feature?.Invoke(featureBuilder);
                return featureBuilder.Build();
            });

            return builder;
        }

        public static ICommunicationBuilder WithDynamicApiClient(this ICommunicationBuilder descriptor, string uri, string template)
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uri);
                b.UseRoute(template);
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient(this ICommunicationBuilder descriptor, Uri uri, string template)
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uri);
                b.UseRoute(template);
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient(this ICommunicationBuilder descriptor, Func<Uri> uriProvider, string template)
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uriProvider);
                b.UseRoute(template);
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient(this ICommunicationBuilder descriptor, Func<string> uriProvider, string template)
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uriProvider);
                b.UseRoute(template);
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient<TRoute>(this ICommunicationBuilder descriptor, string uri)
               where TRoute : class, IRouteControllerProvider
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uri);
                b.UseRoute<TRoute>();
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient<TRoute>(this ICommunicationBuilder descriptor, Uri uri)
               where TRoute : class, IRouteControllerProvider
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uri);
                b.UseRoute<TRoute>();
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient<TRoute>(this ICommunicationBuilder descriptor, Func<string> uriProvider)
             where TRoute : class, IRouteControllerProvider
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uriProvider);
                b.UseRoute<TRoute>();
            });
        }
        public static ICommunicationBuilder WithDynamicApiClient<TRoute>(this ICommunicationBuilder descriptor, Func<Uri> uriProvider)
           where TRoute : class, IRouteControllerProvider
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress(uriProvider);
                b.UseRoute<TRoute>();
            });
        }

        public static ICommunicationBuilder WithDynamicApiClient<TRoute, TUriProvider>(this ICommunicationBuilder descriptor)
           where TRoute : class, IRouteControllerProvider
           where TUriProvider : class, IUriProvider
        {
            return descriptor.WithDynamicApiClient(b =>
            {
                b.UseBaseAddress<TUriProvider>();
                b.UseRoute<TRoute>();
            });
        }
    }
}
