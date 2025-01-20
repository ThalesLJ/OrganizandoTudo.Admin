using MongoDB.Driver;
using OrganizandoTudo.Domain.Entities.Base;
using OrganizandoTudo.Domain.Interfaces.Repositories;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces.Repositories
{
    public interface IBaseMongoRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        IMongoCollection<TEntity> Collection { get; }
        Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter);
        Task<IEnumerable<TEntity>> FindManyAsync(FilterDefinition<TEntity> filter);
        Task<bool> ExistsAsync(FilterDefinition<TEntity> filter);
        Task<long> CountAsync(FilterDefinition<TEntity> filter);
    }
}