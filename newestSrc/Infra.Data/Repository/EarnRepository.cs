using Domain.IRepository;
using Domain.Models.Task;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class EarnRepository:IEarnRepository
{
    private readonly AppDbContext _context;

    public EarnRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTaskForEar(TaskModel model)
    {
        await _context.Tasks.AddAsync(model);
    }

    public async Task AddEarTask(TaskEarnModel model)
    {
        await _context.TaskEarns.AddAsync(model);
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksForEarn()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<IEnumerable<TaskEarnModel>> GetAllEarnTasks()
    {
        return await _context.TaskEarns.ToListAsync();
    }

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }
}