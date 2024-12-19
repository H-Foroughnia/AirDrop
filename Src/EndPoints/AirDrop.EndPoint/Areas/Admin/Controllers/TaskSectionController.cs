using AirDrop.Application.MVC.IService;
using AirDrop.Domain.MVC.IRepository;
using AirDrop.Domain.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirDrop.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class TaskSectionController : BaseController
    {
        private readonly ITaskService _service;
        private readonly ITaskRepository _repository;

        public TaskSectionController(ITaskService service, ITaskRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet("TaskCategory")]
        public async Task<IActionResult> TaskCategory()
        {
            var taskCategories = await _repository.GetAllTaskCategories();
            return View(taskCategories);
        }

        [HttpGet("AddTaskCategory")]
        public IActionResult AddTaskCategory()
        {
            return View();
        }

        [HttpPost("AddTaskCategory")]
        public async Task<IActionResult> AddTaskCategory(AirDropTaskCategoryViewModel taskCategory)
        {
            await _service.AddTaskCategory(taskCategory);
            TempData[SuccessMessage] = "Added successfully.";

            return RedirectToAction("TaskCategory");
        }

        [HttpGet("DeleteTaskCategory")]
        public IActionResult DeleteTaskCategory(long id)
        {
            _repository.DeleteTaskCategory(id);
            TempData[ErrorMessage] = "Deleted successfully.";

            return RedirectToAction("TaskCategory");
        }

        [HttpGet("Task")]
        public async Task<IActionResult> Task()
        {
            var tasks = await _repository.GetAllTasks();
            return View(tasks);
        }

        [HttpGet("AddTask")]
        public async Task<IActionResult> AddTask()
        {
            var taskCategories = await _repository.GetAllTaskCategories();
            var viewModel = new TaskWithCategoriesViewModel()
            {
                TaskCategories = taskCategories
            };

            return View(viewModel);
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(AirDropTaskViewModel viewModel)
        {
            await _service.AddTask(viewModel);
            TempData[SuccessMessage] = "Added successfully.";

            return RedirectToAction("Task");
        }

        [HttpGet("DeleteTask")]
        public IActionResult DeleteTask(long id)
        {
            _repository.DeleteTask(id);
            TempData[ErrorMessage] = "Deleted successfully.";

            return RedirectToAction("TaskCategory");
        }
    }
}