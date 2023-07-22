using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Communication.ClientProxy
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
    public class DisposableProxy<TService> : IClientProxy<TService>
    {
        public DisposableProxy(TService instance)
        {
            Proxy = instance;
        }
        public TService Proxy { get; }

        public void Dispose()
        {
            if (Proxy is IDisposable disp)
                disp.Dispose();

        }
    }



    public static class IClientProxyExtensions
    {
        public static Task CallAsync<TService>(this IClientProxy<TService> source, Action<TService> action)
        {
            using (source)
                return Task.Run(() => action(source.Proxy));
        }
        public static void Call<TService>(this IClientProxy<TService> source, Action<TService> action)
        {
            using (source)
                action(source.Proxy);
        }
        public static Task<TResult> CallAsync<TService, TResult>(this IClientProxy<TService> source, Func<TService, TResult> action)
        {
            using (source)
                return Task.Run(() => action(source.Proxy));
        }
        public static TResult Call<TService, TResult>(this IClientProxy<TService> source, Func<TService, TResult> action)
        {
            using (source)
                return action(source.Proxy);
        }

        public static TResult CallProxy<TService, TResult>(this IServiceProvider provider, Func<TService, TResult> action)
        {
            return provider.GetRequiredService<IClientProxy<TService>>().Call(action);
        }
        public static void CallProxy<TService>(this IServiceProvider provider, Action<TService> action)
        {
              provider.GetRequiredService<IClientProxy<TService>>().Call(action);
        }
    }
}
