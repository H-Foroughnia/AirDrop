using AirDrop.Domain.Models.Task;
using AirDrop.Domain.MVC.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AirDrop.EndPoint.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public CategoryController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<AirDropTaskCategory>> GetAllTaskCategories()
    {
        var categories = await _taskRepository.GetAllTaskCategories();
        if (categories.Any())
        {
            return categories;
        }

        throw new NullReferenceException("No category found");
    }
}