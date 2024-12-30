using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Task
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(16)]
        public string Task { get; set; }
        public override string ToString() { return Task; }
    }
}
