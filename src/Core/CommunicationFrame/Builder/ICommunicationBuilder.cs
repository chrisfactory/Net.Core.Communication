using Microsoft.Extensions.DependencyInjection;
using System;

namespace Net.Core.Communication
{
    public interface ICommunicationBuilder
    {
        ServiceDescriptor ServiceDescriptor { get; }
        IServiceCollection Services { get; }
        ICommunicationFrameDescriptor Build(IServiceProvider rootProvider);
    }
}
