using Domain.Entities;

namespace Application.Services;

public interface IProjectService
{
    Task<Project?> GetProjectAsync(int projectId);
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project> CreateProjectAsync(Project project);
    Task<Project> UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(int projectId);
}
