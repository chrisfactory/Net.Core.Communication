using System;
using System.Collections.Generic;

namespace Communication
{
    public interface ICapabilityDescriptor
    {
        IReadOnlyDictionary<Type, ICapability> Capabilities { get; }
    }
    public static class ICapabilityDescriptorExtensions
    {
        public static TCapability Get<TCapability>(this ICapabilityDescriptor descriptor)
            where TCapability : ICapability
        {
            return (TCapability)descriptor.Capabilities[typeof(TCapability)];
        }
    }
}
