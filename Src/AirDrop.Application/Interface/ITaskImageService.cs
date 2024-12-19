using AirDrop.Domain.DTO.Task;
using AirDrop.Domain.Models.Task;

namespace AirDrop.Application.Interface;

public interface ITaskImageService
{
    Task<AirDropTaskImage> AddTaskImage(AirDropTaskImageDto dto);
}