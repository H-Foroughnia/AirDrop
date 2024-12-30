using Domain.Models.Category;
using Domain.Models.Task;

namespace Domain.IRepository;

public interface IEarnRepository
{
    Task AddTaskForEar(TaskModel model);
    Task AddEarTask(TaskEarnModel model);
    Task<IEnumerable<TaskModel>> GetAllTasksForEarn();
    Task<IEnumerable<TaskEarnModel>> GetAllEarnTasks();
    Task SaveChange();
}