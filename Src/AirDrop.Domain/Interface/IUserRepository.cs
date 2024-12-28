using AirDrop.Domain.Models.User;

namespace AirDrop.Domain.Interface;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(string id);
    Task AddUserAsync(User user);
    Task<User> GetUserByTelegramIdAsync(string telegramId);

    Task SaveChangesAsync();
}