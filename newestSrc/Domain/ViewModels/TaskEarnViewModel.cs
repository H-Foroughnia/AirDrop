using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels;

public class TaskEarnViewModel
{
    public int TaskId { get; set; }
    public IFormFile Icon { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public string Link { get; set; }
}

public enum AddTaskEarnResult
{
    Success,
    Failed
}