using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Status;

namespace Domain.Models.Task
{
    public class ImageTaskDoneModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User.User User { get; set; }
        [ForeignKey("TaskImageModel")] 
        public int TaskId { get; set; }
        public TaskImageModel Task { get; set; }
        public string UploadedImage { get; set; }
        [ForeignKey("StatusTaskModel")] 
        public int StatusId { get; set; }
        public StatusTaskModel Status { get; set; }
        public bool Nude { get; set; } = false; 
        public bool AiValidate { get; set; } = true; 
        [MaxLength(64)] 
        public string ImageHash { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow; 
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
