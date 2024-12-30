using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Category;
using Domain.Models.Label;

namespace Domain.Models.Task
{
    public class TaskImageModel
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey("CategoryTaskModel")] 
        public int CategoryId { get; set; }
        public CategoryTaskModel Category { get; set; }
        [ForeignKey("LabelImageModel")] 
        public int LabelId { get; set; }
        public LabelImageModel Label { get; set; }
        public string SampleImage { get; set; }
        public string ImageDescription { get; set; }
        public string LabelDescription { get; set; }
        public int ImagePoints { get; set; }
        public int LabelPoints { get; set; }

        public override string ToString()
        {
            return $"{Label}";
        }
    }
}
