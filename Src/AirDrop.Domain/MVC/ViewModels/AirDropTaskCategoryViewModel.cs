namespace AirDrop.Domain.MVC.ViewModels;

public class AirDropTaskCategoryViewModel
{
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDelete { get; set; }
}

public enum AddTaskCategoryResult
{
    Success,
    Failed
}