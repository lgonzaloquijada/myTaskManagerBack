using Domain.Entities;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string email, string password);
    }
}