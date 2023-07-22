using Microsoft.Extensions.DependencyInjection;
using System;

namespace Communication
{
    public interface ICommunicationBuilder
    {
        ServiceDescriptor ServiceDescriptor { get; }
        IServiceCollection Services { get; }
        ICommunicationFrameDescriptor Build(IServiceProvider rootProvider);
    }
}
