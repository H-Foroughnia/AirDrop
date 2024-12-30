using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Status
{
    public class StatusTaskModel
    {
        [Key] 
        public int Id { get; set; }
        [MaxLength(16)] 
        public string Status { get; set; }
        public override string ToString() { return Status; }
    }
}
