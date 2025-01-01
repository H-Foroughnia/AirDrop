using System.Security.Claims;
using Domain.IRepository;
using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;
using Infra.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class TaskRepository:ITaskRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddTaskCategory(CategoryTaskModel model)
    {
        await _context.CategoryTasks.AddAsync(model);
    }

    public async Task<TaskImageModel> GetTaskById(int taskId)
    {
        return await _context.TaskImages
            .Include(t => t.Category)
            .Include(t => t.Label)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task AddTaskLabel(LabelImageModel model)
    {
        await _context.LabelImages.AddAsync(model);
    }

    public async Task<IEnumerable<CategoryTaskModel>> GetAllTaskCategories()
    {
        var taskCategories = await _context.CategoryTasks.ToListAsync();
        return taskCategories;
    }

    public async Task<IEnumerable<LabelImageModel>> GetAllTaskLabels()
    {
        var taskLabels = await _context.LabelImages.ToListAsync();
        return taskLabels;
    }

    public async Task DeleteTaskCategory(int id)
    {
        var taskCategory = await _context.CategoryTasks.FindAsync(id);
        if (taskCategory == null)
        {
            throw new KeyNotFoundException($"Task Category with ID {id} not found.");
        }

        _context.CategoryTasks.Remove(taskCategory);
        await _context.SaveChangesAsync();
    }

    public async Task AddTask(TaskImageModel model)
    {
        await _context.TaskImages.AddAsync(model);
    }

    public async Task<IEnumerable<TaskImageModel>> GetAllTasks()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            throw new UnauthorizedAccessException("User not found");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("User not found");
        }

        var completedTaskIds = await _context.TaskDones
            .Where(itd => itd.UserId == userId)
            .Select(itd => itd.TaskId)
            .ToListAsync();

        var tasks = await _context.TaskImages
            .Include(t => t.Category)
            .Include(t => t.Label)
            .Where(t => !completedTaskIds.Contains(t.Id)) 
            .ToListAsync();

        return tasks;
    }


    public async Task UpdateTask(TaskImageModel task)
    {
        _context.TaskImages.Update(task);
        await _context.SaveChangesAsync();
    }

    public void DeleteTask(int id)
    {
        var task = _context.TaskImages.Find(id);
        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        _context.TaskImages.Remove(task);
        _context.SaveChanges();
    }

    public async Task<ImageTaskDoneModel> AddImageTaskAsync(ImageTaskDoneModel imageTask)
    {
        _context.TaskDones.Add(imageTask);
        await _context.SaveChangesAsync();
        return imageTask;
    }

    public async Task<ImageTaskDoneModel> GetImageTaskByIdAsync(int id)
    {
        return await _context.TaskDones
            .Include(x => x.User)
            .Include(x => x.Task)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ImageTaskDoneModel> GetImageTaskByHashAsync(string imageHash)
    {
        return await _context.TaskDones
            .FirstOrDefaultAsync(task => task.ImageHash == imageHash);
    }

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }
}