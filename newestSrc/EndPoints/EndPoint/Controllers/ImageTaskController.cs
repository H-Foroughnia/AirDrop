using Domain.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageTaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public ImageTaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllImageTasks()
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

    }
}
