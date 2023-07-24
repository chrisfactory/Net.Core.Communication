using System.Collections.Generic;

namespace Net.Core.Communication
{
    public interface ICommunicationFrameFilter
    {
        IEnumerable<ICommunicationFrameDescriptor> GetPerimeter();
    }
}
