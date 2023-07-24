using Net.Core.Communication;
using Net.Core.Communication.DynamicApi;
using Net.Core.Communication.DynamicApi.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ICommunicationBuilderExtensions
    {
        public static ICommunicationBuilder WithDynamicApiHosting(this ICommunicationBuilder builder, Action<IDynamicApiHostingFeatureBuilder> feature = null)
        {
            builder.Services.TryAddSingleton<IDynamicApiHostingFeatureBuilder, DynamicApiHostingFeatureBuilder>();

            builder.AddFeature(p =>
            {
                var featureBuilder = p.GetRequiredService<IDynamicApiHostingFeatureBuilder>(); 
                feature?.Invoke(featureBuilder);
                return featureBuilder.Build();
            });

            return builder;
        }

        public static ICommunicationBuilder WithDynamicApiHosting(this ICommunicationBuilder descriptor, string template)
        {
            return descriptor.WithDynamicApiHosting(b => b.UseRoute(template));
        }

        public static ICommunicationBuilder WithDynamicApiHosting<TRoute>(this ICommunicationBuilder descriptor)
               where TRoute : class, IRouteControllerProvider
        {
            return descriptor.WithDynamicApiHosting(b => b.UseRoute<TRoute>());
        }
    }
}
