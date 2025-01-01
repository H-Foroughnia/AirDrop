using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Answer;
using Domain.Models.Status;
using Domain.Models.Task;

namespace Domain.Models.Label
{
    public class LabelTaskDoneModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User.User User { get; set; }
        [ForeignKey("ImageTaskDoneModel")] 
        public int TaskId { get; set; }
        public ImageTaskDoneModel Task { get; set; }
        [ForeignKey("StatusTaskModel")] 
        public int StatusId { get; set; }
        public StatusTaskModel Status { get; set; }
        public bool Answer { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return $"{Task}";
        }
    }
}
