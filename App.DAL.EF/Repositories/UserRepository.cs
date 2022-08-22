using App.Contracts.DAL.Repositories;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserRepository : BaseEntityRepository<AppUser, Domain.Identity.AppUser, AppDbContext>, IUserRepository
{
    public UserRepository(AppDbContext dbContext, IMapper<AppUser, Domain.Identity.AppUser> mapper) : base(dbContext,
        mapper)
    {
    }
}