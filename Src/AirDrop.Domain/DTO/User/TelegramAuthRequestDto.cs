namespace AirDrop.Domain.DTO.User;

public class TelegramAuthRequestDto
{
    public string telegramId { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? username { get; set; }
}