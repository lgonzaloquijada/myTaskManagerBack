using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTOs.AuthDTOs;
using API.DTOs.UserDTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private IConfiguration _configuration { get; }

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    // [HttpPost("register")]
    // public async Task<IActionResult> Register(RegisterDto registerDto)
    // {
    //     var response = await _authService.RegisterAsync(registerDto);
    //     if (!response.Success)
    //     {
    //         return BadRequest(response);
    //     }

    //     return Ok(response);
    // }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _authService.LoginAsync(loginDto.email, loginDto.password);
        if (user == null)
        {
            return BadRequest("Invalid email or password");
        }

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Email)
        };

        var jwSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwSettings["TokenKey"] ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            jwSettings["TokenIssuer"],
            jwSettings["TokenAudience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var userDTO = UserDTO.ToUserDTO(user);
        userDTO.token = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(userDTO);
    }
}
