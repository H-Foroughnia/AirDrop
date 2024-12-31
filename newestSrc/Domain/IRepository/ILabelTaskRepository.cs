using Domain.Models.Label;
using Domain.Models.Task;

namespace Domain.IRepository;

public interface ILabelTaskRepository
{
    Task<IEnumerable<ImageTaskDoneModel>> GetAllLabelTasks();
    Task<LabelTaskDoneModel> AddLabelTaskAsync(LabelTaskDoneModel labelTask);
}