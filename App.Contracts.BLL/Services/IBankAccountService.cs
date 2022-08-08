using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IBankAccountService : IEntityUserService<BankAccount, AppUser>, IBankAccountRepositoryCustom<BankAccount>
{
    
}