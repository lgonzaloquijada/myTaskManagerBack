using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;
public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project?> GetProjectAsync(int projectId)
    {
        var project = await _projectRepository.GetById(projectId);
        return project;
    }

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetAll();
        return projects;
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        await _projectRepository.Create(project);
        return project;
    }

    public async Task<Project> UpdateProjectAsync(Project project)
    {
        await _projectRepository.Update(project);
        return project;
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            throw new Exception($"Project with id {projectId} not found");
        }

        await _projectRepository.Delete(project);
    }
}