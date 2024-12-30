using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Task
{
    public class TaskEarnModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("TaskModel")] 
        public int TaskId { get; set; }
        public TaskModel Task { get; set; }
        [Required]
        [StringLength(255)] 
        public string Icon { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public string Link { get; set; }
        public override string ToString() { return $"{Task}"; }
    }
}
