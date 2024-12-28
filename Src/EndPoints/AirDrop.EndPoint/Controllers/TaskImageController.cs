using AirDrop.Application.Interface;
using AirDrop.Domain.DTO.Task;
using AirDrop.Domain.Interface;
using AirDrop.Domain.Models.Task;
using Microsoft.AspNetCore.Mvc;

namespace AirDrop.EndPoint.Controllers;

[Route("api/taskimage")]
[ApiController]
public class TaskImageController : ControllerBase
{
    private readonly ITaskImageRepository _taskImageRepository;
    private readonly ITaskImageService _taskImageService;

    public TaskImageController(ITaskImageRepository taskImageRepository, ITaskImageService taskImageService)
    {
        _taskImageRepository = taskImageRepository;
        _taskImageService = taskImageService;
    }

    [HttpGet(Name = "GetTaskByCategory")]
    public async Task<List<AirDropTask>> GetTaskByCategory(long categoryId)
    {
        var imageTasks = await _taskImageRepository.GetTaskByCategory(categoryId);
        if (imageTasks.Count >= 1)
        {
            return imageTasks;
        }

        throw new NullReferenceException("No image task by this categoryId found");
    }

    [HttpPost(Name = "DoTask")]
    public async Task<IActionResult> DoTask([FromForm] AirDropTaskImageDto taskImage)
    {
        if (taskImage == null || taskImage.UserId <= 0 || taskImage.TaskId <= 0)
        {
            return BadRequest("Invalid task data provided.");
        }

        try
        {
            var result = await _taskImageService.AddTaskImage(taskImage);

            if (result != null)
            {
                return CreatedAtAction(nameof(GetTaskByCategory), new { categoryId = result.TaskId }, result);
            }

            return BadRequest("Failed to add the task.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}