using OrganizandoTudo.Domain.Entities;
using OrganizandoTudo.Domain.Interfaces.Repositories;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces.Repositories
{
    public interface IUserMongoRepository : IBaseMongoRepository<User>, IUserRepository
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByEmailAsync(string email);
        Task<bool> ExistsByUsernameOrEmailAsync(string username, string email);
        Task<IEnumerable<User>> FindByPartialUsernameAsync(string partialUsername);
        Task UpdateLastLoginAsync(Guid userId);
    }
}