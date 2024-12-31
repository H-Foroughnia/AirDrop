using Application.IService;
using Domain.DTOs.Task;
using Domain.IRepository;
using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Service;

public class TaskService:ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IFileUploadHelper _uploadHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskService(ITaskRepository repository, IFileUploadHelper uploadHelper, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _uploadHelper = uploadHelper;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task UpdateTaskImage(EditTaskImageViewModel viewModel)
    {
        var task = await _repository.GetTaskById(viewModel.Id);
        if (task == null)
        {
            throw new Exception("Task not found.");
        }

        task.CategoryId = viewModel.CategoryId;
        task.LabelId = viewModel.LabelId;

        if (!string.IsNullOrWhiteSpace(viewModel.SampleImage))
        {
            task.SampleImage = viewModel.SampleImage;
        }

        task.ImageDescription = viewModel.ImageDescription;
        task.LabelDescription = viewModel.LabelDescription;
        task.ImagePoints = viewModel.ImagePoints;
        task.LabelPoints = viewModel.LabelPoints;

        await _repository.UpdateTask(task);
    }

    public async Task DoImageTaskAsync(ImageTaskDoneDto imageTask)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("User ID could not be retrieved from the token.");
        }

        string FilePath = _uploadHelper.Upload(imageTask.UploadedImage, "taskSamples");
        var hash = Guid.NewGuid();
        var taskDone = new ImageTaskDoneModel()
        {
            AiValidate = true,
            ImageHash = hash.ToString(),
            Nude = false,
            StatusId = 2,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            TaskId = imageTask.TaskId,
            UploadedImage = FilePath,
            UserId = userId
        };
        await _repository.AddImageTaskAsync(taskDone);
        await _repository.SaveChange();
    }

    public Task<ImageTaskDoneModel> GetImageTaskByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}