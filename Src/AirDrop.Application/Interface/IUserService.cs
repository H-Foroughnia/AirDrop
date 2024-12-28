using AirDrop.Domain.Models.User;

namespace AirDrop.Application.Interface;

public interface IUserService
{
    Task<User> AuthenticateOrRegisterTelegramUserAsync(string telegramId, string firstName, string lastName, string username);
}