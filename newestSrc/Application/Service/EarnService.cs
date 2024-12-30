using Application.IService;
using Domain.IRepository;
using Domain.Models.Category;
using Domain.Models.Task;
using Domain.ViewModels;

namespace Application.Service;

public class EarnService:IEarnService
{
    private readonly IEarnRepository _repository;
    private readonly IFileUploadHelper _uploadHelper;

    public EarnService(IEarnRepository repository, IFileUploadHelper uploadHelper)
    {
        _repository = repository;
        _uploadHelper = uploadHelper;
    }

    public async Task<AddTaskForEarnResult> AddTaskForEarn(TaskViewModel viewModel)
    {
        var taskForEarn = new TaskModel()
        {
            Task = viewModel.Task
        };
        await _repository.AddTaskForEar(taskForEarn);
        await _repository.SaveChange();

        return AddTaskForEarnResult.Success;
    }

    public async Task<AddTaskEarnResult> AddTaskEarn(TaskEarnViewModel viewModel)
    {
        string FilePath = _uploadHelper.Upload(viewModel.Icon, "EarnIcon");

        var taskEarn = new TaskEarnModel()
        {
            Description = viewModel.Description,
            Icon = FilePath,
            Link = viewModel.Link,
            Points = viewModel.Points,
            TaskId = viewModel.TaskId
        };
        await _repository.AddEarTask(taskEarn);
        await _repository.SaveChange();

        return AddTaskEarnResult.Success;
    }
}