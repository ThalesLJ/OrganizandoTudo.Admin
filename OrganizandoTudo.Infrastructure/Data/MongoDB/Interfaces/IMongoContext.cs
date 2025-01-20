using MongoDB.Driver;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
        Task<bool> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task AbortTransactionAsync();
        void AddCommand(Func<Task> func);
    }
}
