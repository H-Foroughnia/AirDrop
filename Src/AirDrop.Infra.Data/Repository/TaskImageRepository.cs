using AirDrop.Domain.Interface;
using AirDrop.Domain.Models.Task;
using AirDrop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AirDrop.Infra.Data.Repository;

public class TaskImageRepository:ITaskImageRepository
{
    private readonly AppDbContext _context;

    public TaskImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AirDropTask>> GetTaskByCategory(long categoryId)
    {
        return await _context.Tasks.Where(t => t.TaskCategoryId == categoryId).ToListAsync();
    }

    public async Task<AirDropTaskImage> AddTask(AirDropTaskImage task)
{
    _context.TaskImages.Add(task);
    await _context.SaveChangesAsync();
    return task;
}

}