using Microsoft.AspNetCore.Builder;

namespace Communication
{
    public interface IPostBuildLoader
    {
        void PostBuild(IApplicationBuilder builder);
    }
}
