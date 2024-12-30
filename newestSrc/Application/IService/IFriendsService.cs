using Domain.Models.Friend;
using Domain.ViewModels;

namespace Application.IService;

public interface IFriendsService
{
    Task<IEnumerable<FriendsModel>> GetAllFriendsAsync();
    Task<FriendsModel> GetFriendByIdAsync(int id);
    Task AddFriendAsync(FriendsViewModel friendsModel);
    Task UpdateFriendAsync(FriendsModel friendsModel);
    Task DeleteFriendAsync(int id);
}