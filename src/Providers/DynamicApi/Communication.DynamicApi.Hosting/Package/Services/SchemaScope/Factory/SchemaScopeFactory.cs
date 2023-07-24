using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Net.Core.Communication.DynamicApi.Hosting
{
    internal class SchemaScopeFactory : ISchemaScopeFactory
    {
        private readonly ICommunicationFrameHostingFilter _frameFilter;
        public SchemaScopeFactory(ICommunicationFrameHostingFilter frameFilter)
        {
            _frameFilter = frameFilter;
        }
        public IReadOnlyDictionary<TypeInfo, ISchemaApi> Create()
        {
            var perimeter = _frameFilter.GetPerimeter();
            var features = perimeter.Select(p => p.GetFeature<IDynamicApiHostingFeature>());
            var boxBuilderToApi = features.Select(f => new BoxApiBuilder(f)).ToList();

            _ = Parallel.ForEach(boxBuilderToApi, box => box.Build());

            return boxBuilderToApi.Select(box => box.ApiBuildResult).ToDictionary(r => r.ServiceType, r => r);
        }





        private class BoxApiBuilder
        {
            private readonly IDynamicApiHostingFeature _feature;
            public BoxApiBuilder(IDynamicApiHostingFeature feature)
            {
                this._feature = feature;
            }

            public ISchemaApi ApiBuildResult { get; private set; }
            public void Build()
            {
                ApiBuildResult = _feature.SchemaApiProvider.Get();
            }
        }
    }
}
