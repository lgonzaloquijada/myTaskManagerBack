using API.DTOs.UserDTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers;

[Authorize]
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
        var usersDTO = users.Select(user => UserDTO.ToUserDTO(user));
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

        var userDTO = UserDTO.ToUserDTO(user);
        return Ok(userDTO);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetByEmail(email);
        if (user == null)
        {
            return NotFound();
        }
        var userDTO = UserDTO.ToUserDTO(user);
        return Ok(userDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDTO userDTO)
    {
        var userModel = userDTO.ToUserModel();
        var createdUser = await _userService.Create(userModel);
        var createUserDTO = UserDTO.ToUserDTO(createdUser);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createUserDTO);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserDTO userDTO)
    {
        var user = userDTO.ToUserModel();
        var updatedUser = await _userService.Update(user);
        var updatedUserDTO = UserDTO.ToUserDTO(updatedUser);
        return Ok(updatedUserDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedUser = await _userService.Delete(id);
        var deletedUserDTO = UserDTO.ToUserDTO(deletedUser);
        return Ok(deletedUserDTO);
    }
}