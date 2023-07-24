using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Net.Core.Communication
{
    public interface ICommunicationFrameDescriptor
    {
        ServiceDescriptor ServiceDescriptor { get; }
    
        IReadOnlyCollection<IUsedScopeResult> Scope { get; }


        TFeature GetFeature<TFeature>() where TFeature : class, IFeature;
        
        bool FeatureIsImplemented<TFeature>() where TFeature : class, IFeature;

        TCapability GetCapability<TFeature, TCapability>() 
            where TFeature : class, IFeature
            where TCapability : class, ICapability;


    }
}
