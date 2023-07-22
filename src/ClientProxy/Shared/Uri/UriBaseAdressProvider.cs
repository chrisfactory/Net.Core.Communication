using System;

namespace Communication.ClientProxy
{
    public class UriBaseAdressProvider : IUriProvider
    {
        public UriBaseAdressProvider(string uri)
        {
            BaseAbdress = new Uri(uri);
        }
        public UriBaseAdressProvider(Uri uri)
        {
            BaseAbdress = uri;
        }
        public UriBaseAdressProvider(Func<string> uri)
        {
            BaseAbdress = new Uri(uri());
        }
        public UriBaseAdressProvider(Func<Uri> uri)
        {
            BaseAbdress = uri();
        }


        public Uri BaseAbdress { get; }
    }
}
