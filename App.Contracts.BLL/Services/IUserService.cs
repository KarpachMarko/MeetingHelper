using App.BLL.DTO.Identity;
using App.Contracts.DAL.Repositories;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserService : IEntityService<AppUser>, IUserRepositoryCustom<AppUser>
{
    
}