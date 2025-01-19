using API.DTOs;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        var usersDTO = users.Select(user => new UserDTO()
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
        });
        return Ok(usersDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetByEmail(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDTO userDTO)
    {
        var userModel = userDTO.ToUserModel();
        var createdUser = await _userService.Create(userModel);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut]
    public async Task<IActionResult> Update(User user)
    {
        var updatedUser = await _userService.Update(user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedUser = await _userService.Delete(id);
        return Ok(deletedUser);
    }
}