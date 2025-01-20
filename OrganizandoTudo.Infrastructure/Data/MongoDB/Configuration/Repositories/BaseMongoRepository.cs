using MongoDB.Driver;
using OrganizandoTudo.Domain.Entities.Base;
using OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Configuration.Repositories
{
    public abstract class BaseMongoRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected BaseMongoRepository(IMongoContext context, string collectionName)
        {
            Context = context;
            DbSet = Context.GetCollection<TEntity>(collectionName);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
            return await DbSet.Find(filter).SingleOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            Context.AddCommand(() => DbSet.InsertOneAsync(entity));
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            Context.AddCommand(() => DbSet.ReplaceOneAsync(filter, entity));
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
            Context.AddCommand(() => DbSet.DeleteOneAsync(filter));
        }
    }
}
