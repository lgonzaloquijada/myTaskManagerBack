using Domain.Entities;

namespace API.DTOs.UserDTOs;

public class UserDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string role { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public string token { get; set; }
    public bool is_active { get; set; }

    public UserDTO()
    {
        id = 0;
        name = string.Empty;
        email = string.Empty;
        password = string.Empty;
        role = string.Empty;
        created_at = DateTime.Now;
        updated_at = DateTime.Now;
        token = string.Empty;
        is_active = true;
    }

    public UserDTO(int id, string name, string email, string password, string role, DateTime created_at, DateTime updated_at, string token, bool is_active)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.role = role;
        this.created_at = created_at;
        this.updated_at = updated_at;
        this.token = token;
        this.is_active = is_active;
    }

    public User ToEntity()
    {
        return new User()
        {
            Id = id,
            Name = name,
            Email = email,
            Password = password,
            Role = role,
            CreatedAt = created_at,
            UpdatedAt = updated_at,
            Token = token,
            IsActive = is_active
        };
    }

    public static UserDTO FromEntity(User user)
    {
        return new UserDTO()
        {
            id = user.Id,
            name = user.Name,
            email = user.Email,
            password = user.Password,
            role = user.Role,
            created_at = user.CreatedAt,
            updated_at = user.UpdatedAt,
            token = user.Token,
            is_active = user.IsActive
        };
    }
}
