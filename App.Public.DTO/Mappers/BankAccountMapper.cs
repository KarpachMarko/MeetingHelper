using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class BankAccountMapper : BaseMapper<BankAccount, BLL.DTO.BankAccount>
{
    public BankAccountMapper(IMapper mapper) : base(mapper)
    {
    }
}