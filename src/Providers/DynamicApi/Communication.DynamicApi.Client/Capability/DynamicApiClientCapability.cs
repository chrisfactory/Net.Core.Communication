using System;

namespace Net.Core.Communication.DynamicApi.Client
{
    public interface IDynamicApiClientCapability : IDynamicApiCapability
    {

    }
    internal class DynamicApiClientCapability : IDynamicApiClientCapability
    {
   
        public Type Type => typeof(IDynamicApiClientCapability); 
        public Type[] RelatedFeatures => new[] { typeof(IDynamicApiClientFeature) };

        public string Name => Type.FullName;
        public string[] Groups => new[] { "client", "dynamic.api" };
    }
}
