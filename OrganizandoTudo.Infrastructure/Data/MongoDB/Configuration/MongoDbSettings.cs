using OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Configuration
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
