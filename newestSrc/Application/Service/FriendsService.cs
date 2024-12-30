using Application.IService;
using Domain.IRepository;
using Domain.Models.Friend;
using Domain.ViewModels;

namespace Application.Service;

public class FriendsService:IFriendsService
{
    private readonly IFriendsRepository _friendsRepository;

    public FriendsService(IFriendsRepository friendsRepository)
    {
        _friendsRepository = friendsRepository;
    }

    public async Task<IEnumerable<FriendsModel>> GetAllFriendsAsync()
    {
        return await _friendsRepository.GetAllAsync();
    }

    public async Task<FriendsModel> GetFriendByIdAsync(int id)
    {
        return await _friendsRepository.GetByIdAsync(id);
    }

    public async Task AddFriendAsync(FriendsViewModel friendsModel)
    {
        var friend = new FriendsModel()
        {
            UserId = friendsModel.UserId,
            FriendId = friendsModel.FriendId
        };
        await _friendsRepository.AddAsync(friend);
    }

    public async Task UpdateFriendAsync(FriendsModel friendsModel)
    {
        await _friendsRepository.UpdateAsync(friendsModel);
    }

    public async Task DeleteFriendAsync(int id)
    {
        await _friendsRepository.DeleteAsync(id);
    }
}