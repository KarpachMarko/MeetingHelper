using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class MoneyTransfer : DomainEntityId
{
    public double Amount { get; set; }
    public DateTime? SentTime { get; set; }
    public DateTime? AcceptedTime { get; set; }
    public EMoneyTransferType? Type { get; set; }

    public Guid SenderId { get; set; }
    public AppUser? Sender { get; set; }
    
    public Guid ReceiverId { get; set; }
    public AppUser? Receiver { get; set; }

    public Guid? ReceiverBankId { get; set; }
    public BankAccount? BankAccount { get; set; }
    
    public Guid? MeetingId { get; set; }
    public Meeting? Meeting { get; set; }
}