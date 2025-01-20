using MongoDB.Driver;
using OrganizandoTudo.Domain.Entities;
using OrganizandoTudo.Domain.Interfaces.Repositories;
using OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Configuration.Repositories
{
    public class UserMongoRepository : BaseMongoRepository<User>, IUserRepository
    {
        public UserMongoRepository(IMongoContext context)
            : base(context, "Users")
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await DbSet.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await DbSet.Find(filter).FirstOrDefaultAsync();
        }
    }
}