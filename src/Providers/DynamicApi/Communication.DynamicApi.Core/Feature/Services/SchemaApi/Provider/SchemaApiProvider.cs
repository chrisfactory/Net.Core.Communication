namespace Communication.DynamicApi
{
    internal class SchemaApiProvider : ISchemaApiProvider
    {
        private readonly ISchemaApiFactory _factory;
        public SchemaApiProvider(ISchemaApiFactory factory)
        {
            _factory = factory;
        }
        public ISchemaApi Get()
        {
            return _factory.Create();
        }
    }
}
