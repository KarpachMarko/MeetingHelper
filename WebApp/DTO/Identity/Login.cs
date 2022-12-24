using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Identity;

public class Login
{
    [StringLength(maximumLength:1028, MinimumLength = 32, ErrorMessage = "Unverified data")]
    public string TelegramData { get; set; } = default!;
    
    [StringLength(maximumLength:64, MinimumLength = 64, ErrorMessage = "Unverified data")]
    public string Hash { get; set; } = default!;
}