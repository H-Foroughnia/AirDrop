using AirDrop.Domain.Models.User;

namespace AirDrop.Application.Interface;

public interface IUserService
{
    Task<User> GetOrCreateUserAsync(long id, string username, string firstName, string lastName, string photoUrl);
}