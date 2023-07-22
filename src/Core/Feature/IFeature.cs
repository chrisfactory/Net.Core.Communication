using System;
using System.Reflection;

namespace Communication
{
    public interface IFeature
    {
        TypeInfo ServiceType { get; }
        Type Type { get; }
        Type RelatedCapability { get; }
    }
}
