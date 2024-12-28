using AirDrop.Application.Interface;
using AirDrop.Domain.Interface;
using AirDrop.Domain.Models.User;

namespace AirDrop.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<User> AuthenticateOrRegisterTelegramUserAsync(string telegramId, string firstName, string lastName, string username)
    {
        var user = await _userRepository.GetUserByTelegramIdAsync(telegramId);

        if (user == null)
        {
            user = new User
            {
                TelegramId = telegramId,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                CreationDate = DateTime.UtcNow,
                IsDelete = false
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        return user;
    }
}