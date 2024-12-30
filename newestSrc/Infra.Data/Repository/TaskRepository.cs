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
        return await _context.TaskImages
            .Include(t => t.Category)
            .Include(t => t.Label)
            .ToListAsync();
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

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }
}