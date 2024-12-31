using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Task;

public class ImageTaskDoneDto
{
    public int TaskId { get; set; }
    public IFormFile UploadedImage { get; set; }
}