using Domain.IRepository;
using Domain.Models.Friend;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class FriendsRepository:IFriendsRepository
{
    private readonly AppDbContext _context;

    public FriendsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FriendsModel>> GetAllAsync()
    {
        return await _context.Friends
            .Include(f => f.User)
            .Include(f => f.Friend)
            .ToListAsync();
    }

    public async Task<FriendsModel> GetByIdAsync(int id)
    {
        return await _context.Friends
            .Include(f => f.User)
            .Include(f => f.Friend)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task AddAsync(FriendsModel friendsModel)
    {
        await _context.Friends.AddAsync(friendsModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FriendsModel friendsModel)
    {
        _context.Friends.Update(friendsModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Friends.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}