using Net.Core.Communication;
using System.Collections.Generic;
using System.Linq;

namespace Net.Core.Communication.DynamicApi.Client
{
    public interface ICommunicationFrameClientFilter : ICommunicationFrameFilter
    {

    }
    internal class CommunicationFrameClientFilter : ICommunicationFrameClientFilter
    {
        private readonly IReadOnlyCollection<ICommunicationFrameDescriptor> _from;
        public CommunicationFrameClientFilter(IEnumerable<ICommunicationFrameDescriptor> from)
        {
            _from = from.ToList();
        }
        public IEnumerable<ICommunicationFrameDescriptor> GetPerimeter()
        {
            foreach (var frame in _from)
            {
                if (frame.Scope.Any(s => s.Capability.Type == typeof(IDynamicApiClientCapability)))
                    yield return frame;
            }
        }
    }
}
