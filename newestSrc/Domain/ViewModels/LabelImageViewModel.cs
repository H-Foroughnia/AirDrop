namespace Domain.ViewModels;

public class LabelImageViewModel
{
    public string Label { get; set; }
}

public enum AddLabelImageResult
{
    Success,
    Failed
}