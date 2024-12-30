using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels;

public class TaskImageViewModel
{
    public int CategoryId { get; set; }
    public int LabelId { get; set; }
    public IFormFile SampleImage { get; set; }
    public string ImageDescription { get; set; }
    public string LabelDescription { get; set; }
    public int ImagePoints { get; set; }
    public int LabelPoints { get; set; }
}

public enum AddTaskImageResult
{
    Success,
    Failed
}