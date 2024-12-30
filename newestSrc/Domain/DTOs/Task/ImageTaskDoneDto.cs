using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Task;

public class ImageTaskDoneDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public IFormFile UploadedImage { get; set; }
    public int StatusId { get; set; }
    public bool Nude { get; set; } = false;
    public bool AiValidate { get; set; } = true;
    public string ImageHash { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}