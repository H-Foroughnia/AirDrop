using Domain.Models.Friend;
using Domain.Models.User;

namespace Application.IService;

public interface IUserService
{
    Task<User> AuthenticateOrRegisterTelegramUserAsync(string telegramId, string firstName, string lastName, string username);
    Task<IEnumerable<User>> GetAllUsersAsync();
}