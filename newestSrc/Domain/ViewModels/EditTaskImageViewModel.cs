using Domain.Models.Category;
using Domain.Models.Label;

namespace Domain.ViewModels;

public class EditTaskImageViewModel
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int LabelId { get; set; }
    public string SampleImage { get; set; }
    public string ImageDescription { get; set; }
    public string LabelDescription { get; set; }
    public int ImagePoints { get; set; }
    public int LabelPoints { get; set; }

    public IEnumerable<CategoryTaskModel> TaskCategories { get; set; }
    public IEnumerable<LabelImageModel> LabelImages { get; set; }
}