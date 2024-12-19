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

    public async Task<User> GetOrCreateUserAsync(long id, string username, string firstName, string lastName, string photoUrl)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            user = new User
            {
                Id = id,
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                PhotoUrl = photoUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        return user;
    }
}