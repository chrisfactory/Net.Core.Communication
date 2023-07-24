using System;
using System.Collections.Generic;

namespace Net.Core.Communication.ClientProxy
{
    public interface IClientProxyProvider
    {
        IClientProxy<TService> Get<TService>();
        IClientProxy<TService> Get<TService>(Func<IReadOnlyList<IClientProxyFactory>, IClientProxyFactory> select);
    }
  
}
