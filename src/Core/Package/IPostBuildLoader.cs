using System;

namespace Net.Core.Communication
{
    public interface IPostBuildLoader
    {
        void PostBuild(IServiceProvider provider);
    }
}
