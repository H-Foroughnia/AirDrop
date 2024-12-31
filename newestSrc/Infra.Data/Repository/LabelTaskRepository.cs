using Domain.IRepository;
using Domain.Models.Label;
using Domain.Models.Task;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class LabelTaskRepository:ILabelTaskRepository
{
    private readonly AppDbContext _context;

    public LabelTaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LabelTaskDoneModel>> GetAllLabelTasks()
    {
        return await _context.LabelTaskDones
            .Include(t => t.Answer)
            .Include(t => t.Status)
            .Include(t => t.Task)
            .ToListAsync();
    }

    public async Task<LabelTaskDoneModel> AddLabelTaskAsync(LabelTaskDoneModel labelTask)
    {
        _context.LabelTaskDones.Add(labelTask);
        await _context.SaveChangesAsync();
        return labelTask;
    }
}