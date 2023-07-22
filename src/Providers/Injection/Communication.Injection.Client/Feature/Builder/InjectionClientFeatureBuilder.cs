using Microsoft.Extensions.DependencyInjection;

namespace Communication.Injection.Client
{
    internal class InjectionClientFeatureBuilder : IInjectionClientFeatureBuilder
    {
        public InjectionClientFeatureBuilder()
        {
            Services = new ServiceCollection();
        }

        public IServiceCollection Services { get; }
        public IInjectionClientFeature Build()
        {
            Services.AddSingleton<IInjectionClientFeature, InjectionClientFeature>();

            var provider = Services.BuildServiceProvider();
            return provider.GetRequiredService<IInjectionClientFeature>();
        }
    }
}
