using Microsoft.Extensions.DependencyInjection;

namespace Net.Core.Communication.Injection.Client
{
    public interface IInjectionClientFeatureBuilder
    {
        IServiceCollection Services { get; }

        IInjectionClientFeature Build();
    }
}
