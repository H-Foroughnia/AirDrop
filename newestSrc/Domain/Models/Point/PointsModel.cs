using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Task;

namespace Domain.Models.Point
{
    public class PointsModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User.User User { get; set; }
        public int Points { get; set; } = 0; 
        [ForeignKey("TaskModel")] 
        public int TaskId { get; set; }
        public TaskModel Task { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
