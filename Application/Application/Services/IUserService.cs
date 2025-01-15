using Domain.Entities;

namespace Application.Services;

public interface IUserService
{
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<User> Create(User user);
    Task<User> Update(User user);
}