using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Label
{
    public class LabelImageModel
    {
        [Key] 
        public int Id { get; set; }
        [MaxLength(16)]
        [Required] 
        public string Label { get; set; }
        public override string ToString() { return Label; }
    }
}
