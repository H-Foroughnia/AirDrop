using Domain.ViewModels;

namespace Application.IService;

public interface ITaskService
{
    Task<AddCategoryTaskResult> AddTaskCategory(CategoryTaskViewModel viewModel);
    Task<AddLabelImageResult> AddLabelImage(LabelImageViewModel viewModel);
    Task<AddTaskImageResult> AddTaskImage(TaskImageViewModel viewModel);
}