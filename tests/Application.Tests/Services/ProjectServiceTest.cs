using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.Servicesl;
public class ProjectServiceTest
{
    private Mock<IProjectRepository> _projectRepositoryMock;
    private ProjectService _projectService;

    public ProjectServiceTest()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _projectService = new ProjectService(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProjectAsync_ShouldReturnProject_WhenProjectExists()
    {
        // Arrange
        var projectId = 1;
        var project = new Project { Id = projectId, Name = "Project 1" };
        _projectRepositoryMock.Setup(x => x.GetById(projectId)).ReturnsAsync(project);

        // Act
        var result = await _projectService.GetProjectAsync(projectId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectId, result.Id);
        Assert.Equal(project.Name, result.Name);
    }

    [Fact]
    public async Task GetProjectAsync_ShouldReturnNull_WhenProjectDoesNotExist()
    {
        // Arrange
        var projectId = 1;
        _projectRepositoryMock.Setup(x => x.GetById(projectId)).ReturnsAsync((Project?)null);

        // Act
        var result = await _projectService.GetProjectAsync(projectId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetProjectAsync_ShouldReturnAllProjects()
    {
        // Arrange
        var projects = new List<Project>
        {
            new() { Id = 1, Name = "Project 1" },
            new() { Id = 2, Name = "Project 2" }
        };
        _projectRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(projects);

        // Act
        var result = await _projectService.GetProjectsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projects.Count, result.Count());
    }

    [Fact]
    public async Task CreateProjectAsync_ShouldCreateProject()
    {
        // Arrange
        var project = new Project { Id = 1, Name = "Project 1" };

        // Act
        var result = await _projectService.CreateProjectAsync(project);

        // Assert
        _projectRepositoryMock.Verify(x => x.Create(project), Times.Once);
        Assert.Equal(project.Id, result.Id);
        Assert.Equal(project.Name, result.Name);
    }
}