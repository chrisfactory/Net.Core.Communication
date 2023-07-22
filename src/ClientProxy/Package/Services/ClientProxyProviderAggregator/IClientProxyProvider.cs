namespace Communication.ClientProxy
{
    public interface IClientProxyProvider
    {
        IClientProxy<TService> Get<TService>();
    }
  
}
