using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Status;

namespace Domain.Models.Task
{
    public class EarnTaskDoneModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User.User User { get; set; }
        [ForeignKey("TaskEarnModel")] 
        public int TaskId { get; set; }
        public TaskEarnModel Task { get; set; }
        [ForeignKey("StatusTaskModel")] 
        public int StatusId { get; set; }
        public StatusTaskModel Status { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow; 
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
