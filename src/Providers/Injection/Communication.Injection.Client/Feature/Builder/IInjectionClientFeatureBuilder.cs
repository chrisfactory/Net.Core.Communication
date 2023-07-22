using Microsoft.Extensions.DependencyInjection;

namespace Communication.Injection.Client
{
    public interface IInjectionClientFeatureBuilder
    {
        IServiceCollection Services { get; }

        IInjectionClientFeature Build();
    }
}
