using System;

namespace Net.Core.Communication.ClientProxy
{
    public interface IUriProvider
    {
        Uri BaseAddress { get; }
    }
}
