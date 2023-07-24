using System;

namespace Net.Core.Communication
{
    public interface ICapability
    {
        Type Type { get; }
        string Name { get; }
        string[] Groups { get; }
        Type[] RelatedFeatures { get; }

    }
}
