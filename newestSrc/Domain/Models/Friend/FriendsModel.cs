using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Friend
{
    public class FriendsModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public virtual User.User User { get; set; }
        [ForeignKey("Friend")] 
        public int FriendId { get; set; }
        public virtual User.User Friend { get; set; }
        public override string ToString() { return User.ToString(); }
    }
}
