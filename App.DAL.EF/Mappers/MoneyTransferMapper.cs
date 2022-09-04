using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MoneyTransferMapper : BaseMapper<MoneyTransfer, Domain.MoneyTransfer>
{
    public MoneyTransferMapper(IMapper mapper) : base(mapper)
    {
    }
}