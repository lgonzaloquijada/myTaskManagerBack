using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.Services;

public class UserServiceTest
{
    private Mock<IUserRepository> _userRepositoryMock;
    private UserService _userService;

    public UserServiceTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new() { Id = 1, Email = "test1@mail.com" },
            new() { Id = 2, Email = "test2@mail.com" }
        };
        _userRepositoryMock.Setup(x => x.GetAll())
            .ReturnsAsync(users);

        // Act
        var result = await _userService.GetAll();

        // Assert
        Assert.Equal(users, result);
    }

    [Fact]
    public async Task GetById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, Email = "test1@mail.com" };
        _userRepositoryMock.Setup(x => x.GetById(1))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetById(1);

        // Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task GetById_WhenUserDoesNotExist_ReturnsNull()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetById(1))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByEmail_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, Email = "test1@mail.com" };
        _userRepositoryMock.Setup(x => x.GetByEmail("test1@mail.com"))
            .ReturnsAsync(user);

        //Act
        var result = await _userService.GetByEmail("test1@mail.com");

        //Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task GetByEmail_WhenUserDoesNotExist_ReturnsNull()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetByEmail("test1@mail.com"))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetByEmail("test1@mail.com");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_WhenEmailIsInUse_ThrowsException()
    {
        // Arrange
        var user = new User { Email = "test1@mail.com" };
        _userRepositoryMock.Setup(x => x.GetByEmail("test1@mail.com"))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _userService.Create(user);

        // Assert
        await Assert.ThrowsAsync<Exception>(act);
    }

    [Fact]
    public async Task Create_WhenEmailIsNotInUse_CreatesUser()
    {
        // Arrange
        var user = new User { Email = "test1@mail.com" };
        _userRepositoryMock.Setup(x => x.GetByEmail("test1@mail.com"))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.Create(user);

        // Assert
        _userRepositoryMock.Verify(x => x.Create(user), Times.Once);
        Assert.Equal("user", user.Role);
        Assert.NotEmpty(user.Password);
        Assert.Empty(user.Token);
    }

    [Fact]
    public async Task Update_UpdatesUser()
    {
        // Arrange
        var user = new User { Id = 1, Email = "test1@mail.com" };

        // Act
        var result = await _userService.Update(user);

        // Assert
        _userRepositoryMock.Verify(x => x.Update(user), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenUserExists_DeletesUser()
    {
        // Arrange
        var user = new User { Id = 1, Email = "test1@mail.com" };
        _userRepositoryMock.Setup(x => x.GetById(1))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.Delete(1);

        // Assert
        _userRepositoryMock.Verify(x => x.Delete(user), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenUserDoesNotExist_ThrowsException()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.GetById(1))
            .ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _userService.Delete(1);

        // Assert
        await Assert.ThrowsAsync<Exception>(act);
    }
}