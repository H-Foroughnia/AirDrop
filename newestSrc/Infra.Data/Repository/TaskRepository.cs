using Domain.IRepository;
using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class TaskRepository:ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTaskCategory(CategoryTaskModel model)
    {
        await _context.CategoryTasks.AddAsync(model);
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
        return await _context.TaskImages.ToListAsync();
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

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }
}