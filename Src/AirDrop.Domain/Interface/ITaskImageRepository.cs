using AirDrop.Domain.Models.Task;

namespace AirDrop.Domain.Interface;

public interface ITaskImageRepository
{
    Task<List<AirDropTask>> GetTaskByCategory(long categoryId);
    Task<AirDropTaskImage> AddTask(AirDropTaskImage task);
}