using Domain.Models.Label;
using Domain.Models.Task;

namespace Domain.IRepository;

public interface ILabelTaskRepository
{
    Task<IEnumerable<LabelTaskDoneModel>> GetAllLabelTasks();
    Task<LabelTaskDoneModel> AddLabelTaskAsync(LabelTaskDoneModel labelTask);
}