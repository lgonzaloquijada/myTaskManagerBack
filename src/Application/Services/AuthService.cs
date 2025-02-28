using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        user.Password = string.Empty;

        return user;
    }
}