using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    public string Username { get; set; } = default!;
}