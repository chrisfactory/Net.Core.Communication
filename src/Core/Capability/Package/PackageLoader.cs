using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Net.Core.Communication
{ 
    internal class PackageLoader : IPackageLoader
    {
        private readonly IReadOnlyCollection<ICapabilityPackage> _packages;
        public PackageLoader(IEnumerable<ICapabilityPackage> packages)
        {
            _packages = packages.ToList();
        }


        public void Load(IServiceCollection services)
        {
            foreach (var package in _packages)
            {
                package.LoadPackage(services);
            }
        }
    }
}
