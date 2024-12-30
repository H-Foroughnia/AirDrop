namespace Domain.ViewModels;

public class CategoryTaskViewModel
{
    public string Category { get; set; }
}

public enum AddCategoryTaskResult
{
    Success,
    Failed
}