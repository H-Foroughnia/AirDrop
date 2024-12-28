using AirDrop.Application.MVC.IService;
using AirDrop.Domain.Models.Task;
using AirDrop.Domain.MVC.IRepository;
using AirDrop.Domain.MVC.ViewModels;
using Task = AirDrop.Domain.Models.Task.AirDropTask;

namespace AirDrop.Application.MVC.Service;

public class TaskService : ITaskService
{
    private readonly IFileUploadHelper _uploadHelper;
    private readonly ITaskRepository _repository;

    public TaskService(IFileUploadHelper uploadHelper, ITaskRepository repository)
    {
        _uploadHelper = uploadHelper;
        _repository = repository;
    }

    public async Task<AddTaskCategoryResult> AddTaskCategory(AirDropTaskCategoryViewModel viewModel)
    {
        var taskCategory = new AirDropTaskCategory()
        {
            Name = viewModel.Name,
            CreationDate = DateTime.UtcNow,
            IsDelete = false
        };
        await _repository.AddTaskCategory(taskCategory);
        await _repository.SaveChange();

        return AddTaskCategoryResult.Success;
    }

    public async Task<AddTaskResult> AddTask(AirDropTaskViewModel viewModel)
    {
        string FilePath = _uploadHelper.Upload(viewModel.SampleImage, "taskSamples");

        var task = new Task()
        {
            TaskCategoryId = viewModel.TaskCategoryId,
            Description = viewModel.Description,
            SampleImage = FilePath,
            Score = viewModel.Score,
            CreationDate = DateTime.UtcNow,
            IsDelete = false
        };
        await _repository.AddTask(task);
        await _repository.SaveChange();

        return AddTaskResult.Success;
    }
}