using Application.IService;
using Domain.DTOs.Label;
using Domain.IRepository;
using Domain.Models.Task;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelTaskController : ControllerBase
    {
        private readonly ILabelTaskService _service;
        private readonly ILabelTaskRepository _repository;

        public LabelTaskController(ILabelTaskService service, ILabelTaskRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet("GetLabelTasks")]
        public async Task<IActionResult> GetLabelTasks()
        {
            try
            {
                var labelTasks = await _repository.GetAllLabelTasks();
                if (labelTasks == null || !labelTasks.Any())
                {
                    return NotFound("No label tasks found.");
                }
                return Ok(labelTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("DoLabelTask")]
        public async Task<ActionResult<ImageTaskDoneModel>> DoLabelTask([FromForm] LabelTaskDoneDto doneDto)
        {
            if (doneDto == null)
            {
                return BadRequest("Invalid label task data.");
            }
            await _service.DoLabelTaskAsync(doneDto);

            return Ok(doneDto);
        }
    }
}
