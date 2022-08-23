using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class BankAccountMapper : BaseMapper<BankAccount, DAL.DTO.BankAccount>
{
    public BankAccountMapper(IMapper mapper) : base(mapper)
    {
    }
}