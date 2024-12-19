using AirDrop.Domain.Models.Task;
using Common.Domain;

namespace AirDrop.Domain.Models.User;

public class User : BaseEntity
{
    public long Id { get; set; } 
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Reward { get; set; }

    public ICollection<AirDropTaskImage> TaskImages { get; set; }
}