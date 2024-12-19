using AirDrop.Application.Interface;
using AirDrop.Application.MVC.IService;
using AirDrop.Domain.DTO.Task;
using AirDrop.Domain.Interface;
using AirDrop.Domain.Models.Task;

namespace AirDrop.Application.Service;

public class TaskImageService : ITaskImageService
{
    private readonly ITaskImageRepository _imageTaskRepository;
    private readonly IFileUploadHelper _uploadHelper;

    public TaskImageService(ITaskImageRepository imageTaskRepository, IFileUploadHelper uploadHelper)
    {
        _imageTaskRepository = imageTaskRepository;
        _uploadHelper = uploadHelper;
    }

    public async Task<AirDropTaskImage> AddTaskImage(AirDropTaskImageDto dto)
    {
        string FilePath = _uploadHelper.Upload(dto.ImageName, "imageTaskSamples");

        var imageTask = new AirDropTaskImage()
        {
            ImageName = FilePath,
            Status = dto.Status,
            TaskId = dto.TaskId,
            UserId = dto.UserId,
            CreationDate = DateTime.Now,
            IsDelete = false
        };
        return await _imageTaskRepository.AddTask(imageTask);
    }
}