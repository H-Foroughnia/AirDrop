using Domain.ViewModels;

namespace Application.IService;

public interface IEarnService
{
    Task<AddTaskForEarnResult> AddTaskForEarn(TaskViewModel viewModel);
    Task<AddTaskEarnResult> AddTaskEarn(TaskEarnViewModel viewModel);
}