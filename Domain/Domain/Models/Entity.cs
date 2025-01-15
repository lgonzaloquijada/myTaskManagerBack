namespace Domain.Models;
public class Entity : ITrackableEntity, IIsActive
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}