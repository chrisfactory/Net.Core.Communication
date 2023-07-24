using System.Collections.Generic;

namespace Net.Core.Communication
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
