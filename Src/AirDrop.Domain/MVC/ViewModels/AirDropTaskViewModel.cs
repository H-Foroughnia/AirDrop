using Microsoft.AspNetCore.Http;

namespace AirDrop.Domain.MVC.ViewModels;

public class AirDropTaskViewModel
{
    public long TaskCategoryId { get; set; }
    public string Description { get; set; }
    public long Score { get; set; }
    public IFormFile SampleImage { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDelete { get; set; }
}

public enum AddTaskResult
{
    Success,
    Failed
}