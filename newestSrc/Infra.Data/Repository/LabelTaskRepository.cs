using System.Security.Claims;
using Domain.IRepository;
using Domain.Models.Label;
using Domain.Models.Task;
using Infra.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class LabelTaskRepository:ILabelTaskRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LabelTaskRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<ImageTaskDoneModel>> GetAllLabelTasks()
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

        var labeledTaskIds = await _context.LabelTaskDones
            .Where(lt => lt.UserId == userId)
            .Select(lt => lt.TaskId)
            .ToListAsync();

        var tasks = await _context.TaskDones
            .Include(t => t.Status)
            .Include(t => t.Task)
            .Where(t => !labeledTaskIds.Contains(t.Id)) 
            .ToListAsync();

        return tasks;
    }


    public async Task<LabelTaskDoneModel> AddLabelTaskAsync(LabelTaskDoneModel labelTask)
    {
        _context.LabelTaskDones.Add(labelTask);
        await _context.SaveChangesAsync();
        return labelTask;
    }
}