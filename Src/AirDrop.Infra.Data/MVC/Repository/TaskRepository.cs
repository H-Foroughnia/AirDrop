using AirDrop.Domain.Models.Task;
using AirDrop.Domain.MVC.IRepository;
using AirDrop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AirDrop.Infra.Data.MVC.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTaskCategory(AirDropTaskCategory model)
    {
        await _context.TaskCategories.AddAsync(model);
    }

    public async Task<IEnumerable<AirDropTaskCategory>> GetAllTaskCategories()
    {
        var taskCategories = await _context.TaskCategories.Where(tc => tc.IsDelete == false).ToListAsync();
        return taskCategories;
    }

    public async Task DeleteTaskCategory(long id)
    {
        var taskCategory = await _context.TaskCategories.FindAsync(id);
        if (taskCategory == null)
        {
            throw new KeyNotFoundException($"Task Category with ID {id} not found.");
        }

        taskCategory.IsDelete = true;
        _context.TaskCategories.Update(taskCategory);
        await _context.SaveChangesAsync();
    }

    public async Task AddTask(AirDropTask model)
    {
        await _context.Tasks.AddAsync(model);
    }

    public async Task<IEnumerable<AirDropTask>> GetAllTasks()
    {
        return await _context.Tasks.Where(t => t.IsDelete == false).ToListAsync();
    }

    public void DeleteTask(long id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        task.IsDelete = true;
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }
}