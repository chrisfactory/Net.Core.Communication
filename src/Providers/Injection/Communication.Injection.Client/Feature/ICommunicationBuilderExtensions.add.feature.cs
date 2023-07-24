using Net.Core.Communication;
using Net.Core.Communication.Injection.Client;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ICommunicationBuilderExtensions
    {
        public static ICommunicationBuilder WithInjectionClient(this ICommunicationBuilder builder)
        {
            builder.Services.TryAddSingleton<IInjectionClientFeatureBuilder, InjectionClientFeatureBuilder>();

            builder.AddFeature(p =>
            {
                var featureBuilder = p.GetRequiredService<IInjectionClientFeatureBuilder>();
                featureBuilder.Services.AddSingleton(builder.ServiceDescriptor);

                return featureBuilder.Build();
            });

            return builder;
        }


    }
}
