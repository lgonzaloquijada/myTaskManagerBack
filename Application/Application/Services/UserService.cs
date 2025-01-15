using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    //private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository/*, ITokenService tokenService*/)
    {
        _userRepository = userRepository;
        //_tokenService = tokenService;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User?> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _userRepository.GetByEmail(email);
    }

    public async Task<User> Create(User user)
    {
        var existingUser = await _userRepository.GetByEmail(user.Email);
        if (existingUser != null)
        {
            throw new Exception("Email already in use");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Role = "user";
        user.Token = ""; // _tokenService.GenerateToken();

        return await _userRepository.Create(user);
    }

    public async Task<User> Update(User user)
    {
        return await _userRepository.Update(user);
    }
}
