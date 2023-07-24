using System;
using System.Reflection;

namespace Net.Core.Communication
{
    public interface IFeature
    {
        TypeInfo ServiceType { get; }
        Type Type { get; }
        Type RelatedCapability { get; }
    }
}
