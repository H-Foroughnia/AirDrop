using Common.Domain;

namespace AirDrop.Domain.Models.Task;

public class AirDropTaskImage : BaseEntity
{
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public string ImageName { get; set; }
    public string Status { get; set; }

    public User.User User { get; set; }
    public AirDropTask Task { get; set; }
}