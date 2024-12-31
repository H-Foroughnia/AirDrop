using Application.IService;
using Domain.DTOs.Label;
using Domain.IRepository;
using Domain.Models.Label;

namespace Application.Service;

public class LabelTaskService:ILabelTaskService
{
    private readonly ILabelTaskRepository _repository;

    public LabelTaskService(ILabelTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task DoLabelTaskAsync(LabelTaskDoneDto labelTask)
    {
        var labelDone = new LabelTaskDoneModel()
        {
            AnswerId = labelTask.AnswerId,
            StatusId = labelTask.StatusId,
            TaskId = labelTask.TaskId,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };
        await _repository.AddLabelTaskAsync(labelDone);
    }
}