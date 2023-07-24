using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Net.Core.Communication
{
    internal class CommunicationFrameDescriptor : ICommunicationFrameDescriptor
    {
        public CommunicationFrameDescriptor(ServiceDescriptor service, IReadOnlyCollection<IUsedScopeResult> scope)
        {
            ServiceDescriptor = service;
            Scope = scope;
        }
        public ServiceDescriptor ServiceDescriptor { get; }
        public IReadOnlyCollection<IUsedScopeResult> Scope { get; }

        public TFeature GetFeature<TFeature>()
            where TFeature : class, IFeature
        {
            var featureType = typeof(TFeature);
            return (TFeature)Scope.Single(t => t.Feature.Type == featureType).Feature;
        }

        public bool FeatureIsImplemented<TFeature>()
            where TFeature : class, IFeature
        {
            var featureType = typeof(TFeature);
            return Scope.Any(t => t.Feature.Type == featureType);
        }

        public TCapability GetCapability<TFeature, TCapability>()
         where TFeature : class, IFeature
         where TCapability : class, ICapability
        {
            var featureType = typeof(TFeature);
            return (TCapability)Scope.Single(t => t.Feature.Type == featureType).Capability;
        }
    }
}
