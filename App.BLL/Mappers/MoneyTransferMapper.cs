using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class MoneyTransferMapper : BaseMapper<MoneyTransfer, DAL.DTO.MoneyTransfer>
{
    public MoneyTransferMapper(IMapper mapper) : base(mapper)
    {
    }
}