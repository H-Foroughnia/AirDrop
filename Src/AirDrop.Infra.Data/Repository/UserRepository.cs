using AirDrop.Domain.Interface;
using AirDrop.Domain.Models.User;
using AirDrop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AirDrop.Infra.Data.Repository;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> GetUserByTelegramIdAsync(string telegramId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}