using Domain.Models.Category;
using Domain.Models.Label;
using Domain.Models.Task;

namespace EndPoint.Areas.Admin.ViewModels;

public class TaskWithCategoriesAndLabelsViewModel
{
    public TaskImageModel Task { get; set; }
    public IEnumerable<CategoryTaskModel> TaskCategories { get; set; }
    public IEnumerable<LabelImageModel> LabelImages { get; set; }
}