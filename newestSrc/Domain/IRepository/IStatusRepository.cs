using Domain.Models.Status;

namespace Domain.IRepository;

public interface IStatusRepository
{
    Task<StatusTaskModel> AddStatusAsync(StatusTaskModel statusTask);
    Task<StatusTaskModel> GetStatusByIdAsync(int id);
    Task<IEnumerable<StatusTaskModel>> GetAllStatusesAsync();
    Task<StatusTaskModel> UpdateStatusAsync(StatusTaskModel statusTask);
}