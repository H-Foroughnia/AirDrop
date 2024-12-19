using AirDrop.Domain.MVC.ViewModels;

namespace AirDrop.Application.MVC.IService;

public interface ITaskService
{
    Task<AddTaskCategoryResult> AddTaskCategory(AirDropTaskCategoryViewModel viewModel);
    Task<AddTaskResult> AddTask(AirDropTaskViewModel viewModel);
}