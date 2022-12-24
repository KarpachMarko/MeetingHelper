using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class BankAccountRepository :
    BaseEntityUserRepository<BankAccount, Domain.BankAccount, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IBankAccountRepository
{
    public BankAccountRepository(AppDbContext dbContext, IMapper<BankAccount, Domain.BankAccount> mapper) : base(
        dbContext, mapper)
    {
    }
}