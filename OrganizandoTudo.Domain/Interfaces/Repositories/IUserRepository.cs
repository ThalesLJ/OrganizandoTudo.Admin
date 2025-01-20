using OrganizandoTudo.Domain.Entities;

namespace OrganizandoTudo.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
    }
}