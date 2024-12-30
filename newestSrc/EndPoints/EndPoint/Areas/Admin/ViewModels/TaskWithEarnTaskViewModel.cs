using Domain.Models.Task;

namespace EndPoint.Areas.Admin.ViewModels;

public class TaskWithEarnTaskViewModel
{
    public TaskEarnModel TaskEarn { get; set; }
    public IEnumerable<TaskModel> Tasks { get; set; }
}