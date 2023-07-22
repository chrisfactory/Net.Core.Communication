using System.Collections.Generic;

namespace Communication
{
    public interface ICommunicationFrameFilter
    {
        IEnumerable<ICommunicationFrameDescriptor> GetPerimeter();
    }
}
