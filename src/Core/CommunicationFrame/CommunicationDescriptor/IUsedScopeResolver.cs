using System.Collections.Generic;

namespace Communication
{
    public interface IUsedScopeResult
    {
        ICapability Capability { get; }
        IFeature Feature { get; }
    }
    public interface IUsedScopeResolver
    {
        IReadOnlyCollection<IUsedScopeResult> Resolve();
    }
}
