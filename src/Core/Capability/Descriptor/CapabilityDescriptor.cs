using System;
using System.Collections.Generic;

namespace Communication
{
    internal class CapabilityDescriptor : ICapabilityDescriptor
    {
        public CapabilityDescriptor(IEnumerable<ICapability> capabilities)
        {
            Capabilities = MakeUnique(capabilities);
        }

        public IReadOnlyDictionary<Type, ICapability> Capabilities { get; }

        private IReadOnlyDictionary<Type, ICapability> MakeUnique(IEnumerable<ICapability> capabilities)
        {
            var uniqueCapabilities = new Dictionary<Type, ICapability>();

            foreach (var capability in capabilities)
            {
                if (uniqueCapabilities.ContainsKey(capability.Type))
                    throw new InvalidOperationException($"{nameof(CapabilityDescriptor)}: Multi definition for communication service: {capability.Type}");
                uniqueCapabilities.Add(capability.Type, capability);

            }

            return uniqueCapabilities;
        }
    }
}
