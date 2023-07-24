using Microsoft.Extensions.DependencyInjection;
using System;

namespace Net.Core.Communication
{
    public static class ICommunicationBuilderExtensions
    {
        public static ICommunicationBuilder AddFeature<TFeature>(this ICommunicationBuilder builder, Func<IServiceProvider, TFeature> implementationFactory)
        where TFeature : class, IFeature
        {
            builder.Services.AddSingleton<IFeature>(implementationFactory);
            return builder;
        }
    }

}
