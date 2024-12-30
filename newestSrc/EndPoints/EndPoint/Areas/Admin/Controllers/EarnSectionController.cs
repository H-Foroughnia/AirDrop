using AirDrop.EndPoint.Areas.Admin.Controllers;
using Application.IService;
using Domain.IRepository;
using Domain.Models.Task;
using Domain.ViewModels;
using EndPoint.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class EarnSectionController : BaseController
    {
        private readonly IEarnRepository _repository;
        private readonly IEarnService _service;

        public EarnSectionController(IEarnRepository repository, IEarnService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet("TaskForEarn")]
        public async Task<IActionResult> TaskForEarn()
        {
            var tasks = await _repository.GetAllTasksForEarn();
            return View(tasks);
        }

        [HttpGet("AddTaskForEarn")]
        public IActionResult AddTaskForEarn()
        {
            return View();
        }

        [HttpPost("AddTaskForEarn")]
        public async Task<IActionResult> AddTaskForEarn(TaskViewModel viewModel)
        {
            await _service.AddTaskForEarn(viewModel);
            TempData[SuccessMessage] = "Added successfully.";
            return RedirectToAction("TaskForEarn");
        }

        [HttpGet("EarnTask")]
        public async Task<IActionResult> EarnTask()
        {
            var earnTasks = await _repository.GetAllEarnTasks();
            return View(earnTasks);
        }

        [HttpGet("AddEarnTask")]
        public async Task<IActionResult> AddEarnTask()
        {
            var tasksForEarn = await _repository.GetAllTasksForEarn();
            var viewModel = new TaskWithEarnTaskViewModel()
            {
                Tasks = tasksForEarn
            };
            return View(viewModel);
        }

        [HttpPost("AddEarnTask")]
        public async Task<IActionResult> AddEarnTask(TaskEarnViewModel viewModel)
        {
            await _service.AddTaskEarn(viewModel);
            TempData[SuccessMessage] = "Added successfully.";

            return RedirectToAction("EarnTask");
        }
    }
}
