﻿using Communication;
using System;

namespace Communication.Injection.Client
{
    public interface IInjectionClientCapability : ICapability
    {

    }
    internal class InjectionClientCapability : IInjectionClientCapability
    {
   
        public Type Type => typeof(IInjectionClientCapability); 
        public Type[] RelatedFeatures => new[] { typeof(IInjectionClientFeature) };

        public string Name => Type.FullName;
        public string[] Groups => new[] { "client", "injection" };
    }
}
