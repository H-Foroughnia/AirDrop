namespace Domain.ViewModels;

public class TaskViewModel
{
    public string Task { get; set; }
}

public enum AddTaskForEarnResult
{
    Success,
    Failed
}