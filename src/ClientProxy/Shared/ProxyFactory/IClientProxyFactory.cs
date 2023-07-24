using System;
using System.Threading.Tasks;

namespace Net.Core.Communication.ClientProxy
{
    public interface IClientProxyFactory
    {
        int PriorityLevel { get; }
        IFeature GetFeature<TService>();
        IClientProxy<TService> Create<TService>();
        bool CanCreate<TService>();
        Type[] GetManagedServices();
    }
    public interface IClientProxy<TService> : IDisposable
    {
        TService Proxy { get; }
    }
  
     
    public static class IClientProxyExtensions
    {
        public static Task CallAsync<TService>(this IClientProxy<TService> source, Action<TService> action)
        {
            return Task.Run(() => action(source.Proxy));
        }

        public static Task<TResult> CallAsync<TService, TResult>(this IClientProxy<TService> source, Func<TService, TResult> action)
        {
            return Task.Run(() => action(source.Proxy));
        }

    }
}
