using System.Diagnostics.CodeAnalysis;
using Domain.Models;

namespace Domain.Entities;

[ExcludeFromCodeCoverage]
public class Project : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public int ManagerId { get; set; }
    public User Manager { get; set; }

    public Project()
    {
        Name = string.Empty;
        Description = string.Empty;
        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
        Status = 0;
        Priority = 0;
        ManagerId = 0;
        Manager = new User();
    }

    public Project(string name, string description, DateTime startDate, DateTime endDate, int status, int priority, int managerId, User manager, ICollection<Task> tasks)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        Priority = priority;
        ManagerId = managerId;
        Manager = manager;
    }
}