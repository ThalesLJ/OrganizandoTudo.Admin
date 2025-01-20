namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
