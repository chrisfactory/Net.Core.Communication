using Net.Core.Communication.ClientProxy;
using Net.Core.Communication.DynamicApi;
using Net.Core.Communication.DynamicApi.Client;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IDynamicApiHostingFeatureBuilderExtensions
    {
     
        public static IDynamicApiClientFeatureBuilder UseBaseAddress(this IDynamicApiClientFeatureBuilder builder, string uri)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAddressProvider(uri));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAddress(this IDynamicApiClientFeatureBuilder builder, Uri uri)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAddressProvider(uri));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAddress(this IDynamicApiClientFeatureBuilder builder, Func<string> uriProvider)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAddressProvider(uriProvider));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAddress(this IDynamicApiClientFeatureBuilder builder, Func<Uri> uriProvider)
        {
            builder.Services.AddSingleton<IUriProvider>(new UriBaseAddressProvider(uriProvider));
            return builder;
        }
        public static IDynamicApiClientFeatureBuilder UseBaseAddress<TUriProvider>(this IDynamicApiClientFeatureBuilder builder)
            where TUriProvider : class, IUriProvider
        {
            builder.Services.AddSingleton<IUriProvider, TUriProvider>();
            return builder;
        }
    }
}
