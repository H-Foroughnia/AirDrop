using Application.IService;
using Domain.DTOs.Task;
using Domain.IRepository;
using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Application.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Service;

public class TaskService:ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IFileUploadHelper _uploadHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository repository, IFileUploadHelper uploadHelper, IHttpContextAccessor httpContextAccessor, ILogger<TaskService> logger)
    {
        _repository = repository;
        _uploadHelper = uploadHelper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
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
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            _logger.LogWarning("User is not authenticated.");
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            _logger.LogWarning("User ID could not be retrieved from the token.");
            throw new UnauthorizedAccessException("User ID could not be retrieved from the token.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            _logger.LogWarning("User ID is not in a valid format.");
            throw new UnauthorizedAccessException("User ID is not in a valid format.");
        }

        _logger.LogInformation("Retrieved User ID from JWT: {UserId}", userId);

        // Upload the image and save the file path
        string filePath = _uploadHelper.Upload(imageTask.UploadedImage, "taskSamples");

        // Load the image using ImageSharp
        using var uploadedImage = SixLabors.ImageSharp.Image.Load($"wwwroot/Uploads/{filePath}");
        string imageHash = uploadedImage.ComputeImageHash();

        // Create and save the task model
        var taskDone = new ImageTaskDoneModel()
        {
            AiValidate = true,
            ImageHash = imageHash,
            Nude = false,
            StatusId = 2,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            TaskId = imageTask.TaskId,
            UploadedImage = filePath,
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