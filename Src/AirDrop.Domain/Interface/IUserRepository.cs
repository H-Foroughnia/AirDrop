using AirDrop.Domain.Models.User;

namespace AirDrop.Domain.Interface;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(long id);
    Task AddUserAsync(User user);
    Task SaveChangesAsync();
}