using Application.IService;
using Domain.DTOs.Task;
using Domain.IRepository;
using Domain.Models.Task;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageTaskController : ControllerBase
    {
        private readonly ITaskService _service;
        private readonly ITaskRepository _taskRepository;

        public ImageTaskController(ITaskRepository taskRepository, ITaskService service)
        {
            _taskRepository = taskRepository;
            _service = service;
        }

        [HttpGet("GetImageTasks")]
        public async Task<IActionResult> GetImageTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetAllTasks();
                if (tasks == null || !tasks.Any())
                {
                    return NotFound("No tasks found.");
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("DoImageTask")]
        public async Task<ActionResult<ImageTaskDoneModel>> DoImageTask([FromForm] ImageTaskDoneDto doneDto)
        {
            if (doneDto == null)
            {
                return BadRequest("Invalid image task data.");
            }

            await _service.DoImageTaskAsync(doneDto);
            return Ok(doneDto);
        }
    }
}
