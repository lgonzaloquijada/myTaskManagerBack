namespace Domain.Models;
public interface ITrackableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    int CreatedBy { get; set; }
    int UpdatedBy { get; set; }
}