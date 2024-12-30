using Domain.IRepository;
using Domain.Models.Status;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class StatusRepository:IStatusRepository
{
    private readonly AppDbContext _context;

    public StatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StatusTaskModel> AddStatusAsync(StatusTaskModel statusTask)
    {
        await _context.AddAsync(statusTask);
        await _context.SaveChangesAsync();
        return statusTask;
    }

    public async Task<StatusTaskModel> GetStatusByIdAsync(int id)
    {
        return await _context.StatusTasks.FindAsync(id);
    }

    public async Task<IEnumerable<StatusTaskModel>> GetAllStatusesAsync()
    {
        return await _context.StatusTasks.ToListAsync();
    }

    public async Task<StatusTaskModel> UpdateStatusAsync(StatusTaskModel statusTask)
    {
        _context.Update(statusTask);
        await _context.SaveChangesAsync();
        return statusTask;
    }
}