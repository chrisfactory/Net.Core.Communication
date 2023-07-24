using System;
using System.Collections.Generic;

namespace Net.Core.Communication
{
    public interface ICapabilityDescriptor
    {
        IReadOnlyDictionary<Type, ICapability> Capabilities { get; }
    }
}
