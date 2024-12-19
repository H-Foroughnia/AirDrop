using Common.Domain;

namespace AirDrop.Domain.Models.Task;

public class AirDropTaskApprovement:BaseEntity
{
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public bool IsApproved { get; set; }
}