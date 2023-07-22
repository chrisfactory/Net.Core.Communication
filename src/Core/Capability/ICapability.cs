using System;

namespace Communication
{
    public interface ICapability
    {
        Type Type { get; }
        string Name { get; }
        string[] Groups { get; }
        Type[] RelatedFeatures { get; }

    }
}
