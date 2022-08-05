using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    [MaxLength(128)]
    public string TelegrammId { get; set; } = default!;
    
    public ICollection<MeetingUser>? MeetingUsers { get; set; }
    public ICollection<EventUser>? EventUsers { get; set; }
    public ICollection<RequirementUser>? RequirementUsers { get; set; }
    public ICollection<Payment>? Payments { get; set; }
    public ICollection<MoneyTransfer>? SendTransfers { get; set; }
    public ICollection<MoneyTransfer>? ReceiveTransfers { get; set; }
    public ICollection<BankAccount>? BankAccounts { get; set; }
}