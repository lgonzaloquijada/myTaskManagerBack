using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    private readonly MainContext _context;

    public ProjectRepository(MainContext context) : base(context)
    {
        _context = context;
    }
}