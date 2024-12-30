using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Category
{
    public class CategoryTaskModel
    {
        [Key] 
        public int Id { get; set; }
        [MaxLength(16)]
        [Required] 
        public string Category { get; set; }
        public override string ToString() { return Category; }
    }
}
