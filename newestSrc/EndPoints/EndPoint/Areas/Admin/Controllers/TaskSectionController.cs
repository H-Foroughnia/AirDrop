﻿using AirDrop.EndPoint.Areas.Admin.Controllers;
using Application.IService;
using Domain.IRepository;
using Domain.ViewModels;
using EndPoint.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class TaskSectionController : BaseController
    {
        private readonly ITaskRepository _repository;
        private readonly ITaskService _service;

        public TaskSectionController(ITaskRepository repository, ITaskService service)
        {
            _repository = repository;
            _service = service;
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
        public async Task<IActionResult> AddTaskCategory(CategoryTaskViewModel taskCategory)
        {
            await _service.AddTaskCategory(taskCategory);
            TempData[SuccessMessage] = "Added successfully.";

            return RedirectToAction("TaskCategory");
        }

        [HttpGet("TaskLabel")]
        public async Task<IActionResult> TaskLabel()
        {
            var taskLabels = await _repository.GetAllTaskLabels();
            return View(taskLabels);
        }

        [HttpGet("AddTaskLabel")]
        public IActionResult AddTaskLabel()
        {
            return View();
        }

        [HttpPost("AddTaskLabel")]
        public async Task<IActionResult> AddTaskLabel(LabelImageViewModel labelImage)
        {
            await _service.AddLabelImage(labelImage);
            TempData[SuccessMessage] = "Added successfully.";

            return RedirectToAction("TaskLabel");
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
            var taskLabels = await _repository.GetAllTaskLabels();
            var viewModel = new TaskWithCategoriesAndLabelsViewModel()
            {
                TaskCategories = taskCategories,
                LabelImages = taskLabels
            };

            return View(viewModel);
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(TaskImageViewModel viewModel)
        {
            await _service.AddTaskImage(viewModel);
            TempData[SuccessMessage] = "Added successfully.";

            return RedirectToAction("Task");
        }
    }
}