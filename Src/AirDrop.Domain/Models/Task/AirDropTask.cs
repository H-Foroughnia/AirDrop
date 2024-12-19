using Common.Domain;

namespace AirDrop.Domain.Models.Task;

public class AirDropTask: BaseEntity
{
    public string Description { get; set; }
    public long Score { get; set; }
    public long TaskCategoryId { get; set; }
    public string SampleImage { get; set; }

    public ICollection<AirDropTaskImage> TaskImages { get; set; }
    public AirDropTaskCategory TaskCategory { get; set; }
}