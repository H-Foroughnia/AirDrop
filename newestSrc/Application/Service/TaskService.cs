using Application.IService;
using Domain.IRepository;
using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;
using Domain.ViewModels;

namespace Application.Service;

public class TaskService:ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IFileUploadHelper _uploadHelper;

    public TaskService(ITaskRepository repository, IFileUploadHelper uploadHelper)
    {
        _repository = repository;
        _uploadHelper = uploadHelper;
    }

    public async Task<AddCategoryTaskResult> AddTaskCategory(CategoryTaskViewModel viewModel)
    {
        var categoryTask = new CategoryTaskModel()
        {
            Category = viewModel.Category
        };
        await _repository.AddTaskCategory(categoryTask);
        await _repository.SaveChange();

        return AddCategoryTaskResult.Success;
    }

    public async Task<AddLabelImageResult> AddLabelImage(LabelImageViewModel viewModel)
    {
        var labelImage = new LabelImageModel()
        {
            Label = viewModel.Label
        };
        await _repository.AddTaskLabel(labelImage);
        await _repository.SaveChange();

        return AddLabelImageResult.Success;
    }

    public async Task<AddTaskImageResult> AddTaskImage(TaskImageViewModel viewModel)
    {
        string FilePath = _uploadHelper.Upload(viewModel.SampleImage, "taskSamples");

        var task = new TaskImageModel()
        {
            CategoryId = viewModel.CategoryId,
            LabelId = viewModel.LabelId,
            SampleImage = FilePath,
            ImageDescription = viewModel.ImageDescription,
            LabelDescription = viewModel.LabelDescription,
            ImagePoints = viewModel.ImagePoints,
            LabelPoints = viewModel.LabelPoints
        };
        await _repository.AddTask(task);
        await _repository.SaveChange();

        return AddTaskImageResult.Success;
    }
}