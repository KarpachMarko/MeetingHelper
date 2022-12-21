using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.Public.DTO.v1.Identity;

public class AppUser : BaseUser
{
    [MaxLength(128)]
    public string? TelegramId { get; set; }
    
    [MaxLength(128)]
    public string? FirstName { get; set; }
    
    [MaxLength(128)]
    public string? LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}