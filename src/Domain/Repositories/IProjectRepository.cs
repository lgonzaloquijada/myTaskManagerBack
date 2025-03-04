using Domain.Entities;

namespace Domain.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAll();
    Task<Project?> GetById(int id);
    Task<Project> Create(Project project);
    Task<Project> Update(Project project);
    Task<Project> Delete(Project project);
}
