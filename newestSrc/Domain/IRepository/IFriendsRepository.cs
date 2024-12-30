using Domain.Models.Friend;

namespace Domain.IRepository;

public interface IFriendsRepository
{
    Task<IEnumerable<FriendsModel>> GetAllAsync();
    Task<FriendsModel> GetByIdAsync(int id);
    Task AddAsync(FriendsModel friendsModel);
    Task UpdateAsync(FriendsModel friendsModel);
    Task DeleteAsync(int id);
}