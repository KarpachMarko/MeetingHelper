using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class MoneyTransfer : DomainEntityId
{
    public double Amount { get; set; }
    public DateTime? SentTime { get; set; }
    public DateTime? AcceptedTime { get; set; }
    public EMoneyTransferType? Type { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid? ReceiverBankId { get; set; }

    public Guid? MeetingId { get; set; }
}