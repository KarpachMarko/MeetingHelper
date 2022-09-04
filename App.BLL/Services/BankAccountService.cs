using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class BankAccountService :
    BaseEntityUserService<BankAccount, DAL.DTO.BankAccount, AppUser, DAL.DTO.Identity.AppUser, IBankAccountRepository>,
    IBankAccountService
{
    public BankAccountService(IBankAccountRepository repository, IMapper<BankAccount, DAL.DTO.BankAccount> mapper) : base(repository, mapper)
    {
    }
}