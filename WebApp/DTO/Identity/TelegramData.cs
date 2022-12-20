namespace WebApp.DTO.Identity;

public class TelegramData
{
    public string TelegramId { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}