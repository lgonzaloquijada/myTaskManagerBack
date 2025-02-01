using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.Services;

public class AuthServiceTest
{
    private Mock<IUserRepository> _userRepositoryMock;
    private AuthService _authService;

    public AuthServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _authService = new AuthService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task LoginAsync_WhenUserDoesNotExist_ReturnsNull()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _authService.LoginAsync("shouldntexist", "password");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_WhenPasswordIsIncorrect_ReturnsNull()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync(new User
            {
                Email = "test@mail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password")
            });

        // Act
        var result = await _authService.LoginAsync("test@mail.com", "wrongpassword");

        // Assert
        Assert.Null(result);
    }
}
