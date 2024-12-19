using Common.Domain;

namespace AirDrop.Domain.Models.Task;

public class AirDropTaskCategory : BaseEntity
{
    public string Name { get; set; }

    public ICollection<AirDropTask> Tasks { get; set; }
}