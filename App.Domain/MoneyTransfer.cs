using App.Domain.Enums;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class MoneyTransfer : DomainEntityId
{
    public float Amount { get; set; }
    
    public DateTime SentTime { get; set; }
    
    public DateTime AcceptedTime { get; set; }

    public MoneyTransferType Type { get; set; }

    public Guid SenderId { get; set; }
    public AppUser? Sender { get; set; }
    
    public Guid ReceiverId { get; set; }
    public AppUser? Receiver { get; set; }

    public Guid? ReceiverBankId { get; set; }
    public BankAccount? BankAccount { get; set; }
}