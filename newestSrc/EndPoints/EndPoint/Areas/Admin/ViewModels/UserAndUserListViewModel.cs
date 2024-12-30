using Domain.Models.Friend;
using Domain.Models.User;

namespace EndPoint.Areas.Admin.ViewModels;

public class UserAndUserListViewModel
{
    public User User { get; set; }
    public IEnumerable<User> UserList { get; set; }
    public FriendsModel Friend  { get; set; }
}