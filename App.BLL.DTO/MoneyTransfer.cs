using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using App.Domain.Enums;
using Base.Domain;

namespace App.BLL.DTO;

public class MoneyTransfer : DomainEntityId
{
    public double Amount { get; set; }
    public DateTime? SentTime { get; set; }
    public DateTime? AcceptedTime { get; set; }
    public EMoneyTransferType? Type { get; set; }

    public Guid SenderId { get; set; }
    [Required]
    public AppUser? Sender { get; set; }
    
    public Guid ReceiverId { get; set; }
    [Required]
    public AppUser? Receiver { get; set; }

    public Guid? ReceiverBankId { get; set; }
    public BankAccount? BankAccount { get; set; }
    
    public Guid? MeetingId { get; set; }
    public Meeting? Meeting { get; set; }
}