using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IBankAccountRepository : IEntityUserRepository<BankAccount, AppUser>, IBankAccountRepositoryCustom<BankAccount>
{
    
}

public interface IBankAccountRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}