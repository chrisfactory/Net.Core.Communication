using System;

namespace Net.Core.Communication.ClientProxy
{
    public class UriBaseAddressProvider : IUriProvider
    {
        public UriBaseAddressProvider(string uri)
        {
            BaseAddress = new Uri(uri);
        }
        public UriBaseAddressProvider(Uri uri)
        {
            BaseAddress = uri;
        }
        public UriBaseAddressProvider(Func<string> uri)
        {
            BaseAddress = new Uri(uri());
        }
        public UriBaseAddressProvider(Func<Uri> uri)
        {
            BaseAddress = uri();
        }


        public Uri BaseAddress { get; }
    }
}
