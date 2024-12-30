using Domain.DTOs.Task;
using Domain.Models.Task;
using Domain.ViewModels;

namespace Application.IService;

public interface ITaskService
{
    Task<AddCategoryTaskResult> AddTaskCategory(CategoryTaskViewModel viewModel);
    Task<AddLabelImageResult> AddLabelImage(LabelImageViewModel viewModel);
    Task<AddTaskImageResult> AddTaskImage(TaskImageViewModel viewModel);
    Task UpdateTaskImage(EditTaskImageViewModel viewModel);
    Task DoImageTaskAsync(ImageTaskDoneDto imageTask);
    Task<ImageTaskDoneModel> GetImageTaskByIdAsync(int id);
}