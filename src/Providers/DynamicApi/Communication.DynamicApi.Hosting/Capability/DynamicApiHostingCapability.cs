using System;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    public interface IDynamicApiHostingCapability : IDynamicApiCapability
    {

    }
    internal class DynamicApiHostingCapability : IDynamicApiHostingCapability
    {
   
        public Type Type => typeof(IDynamicApiHostingCapability); 
        public Type[] RelatedFeatures => new[] { typeof(IDynamicApiHostingFeature) };

        public string Name => Type.FullName;
        public string[] Groups => new[] { "hosting", "dynamic.api" };
    }
}
