using System.Collections.Generic;
using System.Linq;

namespace Communication
{
    internal class UsedScopeResolver : IUsedScopeResolver
    {
        private readonly ICapabilityDescriptor _capabilities;
        private readonly IReadOnlyList<IFeature> _features;
        public UsedScopeResolver(ICapabilityDescriptor capabilities, IEnumerable<IFeature> features)
        {

            this._capabilities = capabilities;
            this._features = features.ToList();
        }
        public IReadOnlyCollection<IUsedScopeResult> Resolve()
        {
            var results = new List<IUsedScopeResult>();

            foreach (var usedFeature in this._features)
                if (_capabilities.Capabilities.ContainsKey(usedFeature.RelatedCapability))
                    results.Add(new UsedScopeResult(usedFeature, _capabilities.Capabilities[usedFeature.RelatedCapability]));

            return results;
        }

        private class UsedScopeResult : IUsedScopeResult
        {
            public UsedScopeResult(IFeature usedFeature, ICapability usedCapability)
            {
                Capability = usedCapability;
                Feature = usedFeature;
            }
            public ICapability Capability { get; }
            public IFeature Feature { get; }
        }
    }
}
