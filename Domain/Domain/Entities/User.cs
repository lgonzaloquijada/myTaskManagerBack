using Domain.Models;

namespace Domain.Entities;
public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }

    public User(string name, string email, string password, string role, string token)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        Token = token;
    }
}