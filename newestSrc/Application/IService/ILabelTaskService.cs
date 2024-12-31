using Domain.DTOs.Label;
using Domain.DTOs.Task;

namespace Application.IService;

public interface ILabelTaskService
{
    Task DoLabelTaskAsync(LabelTaskDoneDto labelTask);
}