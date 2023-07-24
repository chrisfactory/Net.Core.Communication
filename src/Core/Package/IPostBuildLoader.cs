using Microsoft.AspNetCore.Builder;

namespace Net.Core.Communication
{
    public interface IPostBuildLoader
    {
        void PostBuild(IApplicationBuilder builder);
    }
}
