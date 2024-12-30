using Domain.Models.Friend;
using Domain.Models.User;

namespace Domain.IRepository;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User> GetUserByTelegramIdAsync(string telegramId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task SaveChangesAsync();
}