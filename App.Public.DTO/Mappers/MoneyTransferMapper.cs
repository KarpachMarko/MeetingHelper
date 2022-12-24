using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class MoneyTransferMapper : BaseMapper<MoneyTransfer, BLL.DTO.MoneyTransfer>
{
    public MoneyTransferMapper(IMapper mapper) : base(mapper)
    {
    }
}