using Domain.Entities;

namespace Domain.Repositories;
public interface IUserRepository
{
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<User> Create(User user);
    Task<User> Update(User user);
}
