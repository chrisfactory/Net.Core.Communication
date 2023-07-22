using Communication;
using System.Collections.Generic;
using System.Linq;

namespace Communication.Injection.Client
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
                if (frame.Scope.Any(s => s.Capability.Type == typeof(IInjectionClientCapability)))
                    yield return frame;
            }
        }
    }
}
