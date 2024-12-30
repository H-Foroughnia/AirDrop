using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;

namespace Domain.IRepository;

public interface ITaskRepository
{
    Task AddTaskCategory(CategoryTaskModel model);
    Task<TaskImageModel> GetTaskById(int taskId);
    Task AddTaskLabel(LabelImageModel model);
    Task<IEnumerable<CategoryTaskModel>> GetAllTaskCategories();
    Task<IEnumerable<LabelImageModel>> GetAllTaskLabels();
    Task DeleteTaskCategory(int id);
    Task AddTask(TaskImageModel model);
    Task<IEnumerable<TaskImageModel>> GetAllTasks();
    Task UpdateTask(TaskImageModel task);
    void DeleteTask(int id);

    Task<ImageTaskDoneModel> AddImageTaskAsync(ImageTaskDoneModel imageTask);
    Task<ImageTaskDoneModel> GetImageTaskByIdAsync(int id);

    Task SaveChange();
}