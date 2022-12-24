using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class BankAccountMapper : BaseMapper<BankAccount, Domain.BankAccount>
{
    public BankAccountMapper(IMapper mapper) : base(mapper)
    {
    }
}