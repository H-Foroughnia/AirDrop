namespace Domain.DTOs.Label;

public class LabelTaskDoneDto
{
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public int StatusId { get; set; }
    public int AnswerId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}