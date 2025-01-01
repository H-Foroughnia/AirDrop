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
using Common.Application;
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

    public async Task<ServiceResponse> DoImageTaskAsync(ImageTaskDoneDto imageTask)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            _logger.LogWarning("User is not authenticated.");
            return new ServiceResponse
            {
                Success = false,
                Message = "User is not authenticated.",
                StatusCode = 401
            };
        }

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            _logger.LogWarning("User ID could not be retrieved from the token.");
            return new ServiceResponse
            {
                Success = false,
                Message = "User ID could not be retrieved from the token.",
                StatusCode = 401
            };
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            _logger.LogWarning("User ID is not in a valid format.");
            return new ServiceResponse
            {
                Success = false,
                Message = "User ID is not in a valid format.",
                StatusCode = 401
            };
        }
        _logger.LogInformation("Retrieved User ID from JWT: {UserId}", userId);

        string filePath = _uploadHelper.Upload(imageTask.UploadedImage, "taskSamples");
        using var uploadedImage = SixLabors.ImageSharp.Image.Load($"wwwroot/Uploads/{filePath}");
        string imageHash = uploadedImage.ComputeImageHash();

        var existingTask = await _repository.GetImageTaskByHashAsync(imageHash);
        if (existingTask != null)
        {
            _logger.LogWarning("Image with hash {ImageHash} already exists.", imageHash);
            return new ServiceResponse
            {
                Success = false,
                Message = "This image already exists.",
                StatusCode = 409
            };
        }

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

        return new ServiceResponse
        {
            Success = true,
            Message = "Image task processed successfully.",
            StatusCode = 200
        };
    }



    public Task<ImageTaskDoneModel> GetImageTaskByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}