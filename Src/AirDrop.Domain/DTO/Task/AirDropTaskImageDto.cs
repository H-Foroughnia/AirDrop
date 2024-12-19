using Microsoft.AspNetCore.Http;

namespace AirDrop.Domain.DTO.Task;

public class AirDropTaskImageDto
{
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public IFormFile ImageName { get; set; }
    public string Status { get; set; }
}