using AirDrop.Domain.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace AirDrop.Domain.MVC.IRepository;

public interface ITaskRepository
{
    Task AddTaskCategory(AirDropTaskCategory model);
    Task<IEnumerable<AirDropTaskCategory>> GetAllTaskCategories();
    Task DeleteTaskCategory(long id);
    Task AddTask(AirDropTask model);
    Task<IEnumerable<AirDropTask>> GetAllTasks();
    void DeleteTask(long id);
    Task SaveChange();
}