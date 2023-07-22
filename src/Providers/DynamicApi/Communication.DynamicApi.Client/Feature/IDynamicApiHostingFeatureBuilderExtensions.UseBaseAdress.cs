using Communication.ClientProxy;
using Communication.DynamicApi;
using Communication.DynamicApi.Client;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IDynamicApiHostingFeatureBuilderExtensions
    {
     
        public static IDynamicApiClientFeatureBuilder UseBaseAdress(this IDynamicApiClientFeatureBuilder builder, string uri)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAdressProvider(uri));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAdress(this IDynamicApiClientFeatureBuilder builder, Uri uri)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAdressProvider(uri));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAdress(this IDynamicApiClientFeatureBuilder builder, Func<string> uriProvider)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAdressProvider(uriProvider));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAdress(this IDynamicApiClientFeatureBuilder builder, Func<Uri> uriProvider)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAdressProvider(uriProvider));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAdress<TUriProvider>(this IDynamicApiClientFeatureBuilder builder)
            where TUriProvider : class, IUriProvider
        {
            builder.Services.AddSingleton<IUriProvider, TUriProvider>();
            return builder;
        }
    }
}
