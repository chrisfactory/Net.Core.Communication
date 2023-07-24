using Net.Core.Communication;
using System.Collections.Generic;
using System.Linq;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    public interface ICommunicationFrameHostingFilter : ICommunicationFrameFilter
    {

    }
    internal class CommunicationFrameHostingFilter : ICommunicationFrameHostingFilter
    {
        private readonly IReadOnlyCollection<ICommunicationFrameDescriptor> _from;
        public CommunicationFrameHostingFilter(IEnumerable<ICommunicationFrameDescriptor> from)
        {
            _from = from.ToList();
        }
        public IEnumerable<ICommunicationFrameDescriptor> GetPerimeter()
        {
            foreach (var frame in _from)
            {
                if (frame.Scope.Any(s => s.Capability.Type == typeof(IDynamicApiHostingCapability)))
                    yield return frame;
            }
        }
    }
}
