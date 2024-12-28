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


    public async Task<User> AuthenticateOrRegisterTelegramUserAsync(string id, string first_name, string last_name, string username)
    {
        var user = await _userRepository.GetUserByTelegramIdAsync(id);

        if (user == null)
        {
            user = new User
            {
                TelegramId = id,
                FirstName = first_name,
                LastName = last_name,
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