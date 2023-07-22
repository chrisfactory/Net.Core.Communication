using System;
using System.Linq;
using System.Reflection;

namespace Communication.DynamicApi.Hosting
{
    public interface IProxy
    {
        void Invoke();
        void Invoke<TRequest>(TRequest request);
        TResult Invoke<TResult>();
        TResult Invoke<TResult, TRequest>(TRequest request);
    }
    public interface IHostProxyController<T> : IProxy
    {

    }


    internal static class IProxyTouch
    {
        static IProxyTouch()
        {

            foreach (var met in typeof(IProxy).GetMethods())
            {
                var withParam = met.GetParameters().Count() > 0;
                if (met.ReturnType == typeof(void))
                {
                    if (withParam)
                        VoidInvokeRequest = met;
                    else
                        VoidInvoke = met;
                }
                else
                {
                    if (withParam)
                        TypeInvokeRequest = met;
                    else
                        TypeInvoke = met;
                }
            }
            if (VoidInvoke == null)
                throw new InvalidOperationException(nameof(VoidInvoke));
            if (VoidInvokeRequest == null)
                throw new InvalidOperationException(nameof(VoidInvokeRequest));
            if (TypeInvoke == null)
                throw new InvalidOperationException(nameof(TypeInvoke));
            if (TypeInvokeRequest == null)
                throw new InvalidOperationException(nameof(TypeInvokeRequest));
        }


        internal static readonly MethodInfo VoidInvoke;
        internal static readonly MethodInfo VoidInvokeRequest;
        internal static readonly MethodInfo TypeInvoke;
        internal static readonly MethodInfo TypeInvokeRequest;
    }


}