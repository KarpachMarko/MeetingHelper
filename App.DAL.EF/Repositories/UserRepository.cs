using App.Contracts.DAL.Repositories;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserRepository : BaseEntityRepository<AppUser, Domain.Identity.AppUser, AppDbContext>, IUserRepository
{
    public UserRepository(AppDbContext dbContext, IMapper<AppUser, Domain.Identity.AppUser> mapper) : base(dbContext,
        mapper)
    {
    }

    public async Task<Guid?> GetByTgId(string userTgId)
    {
        return (await CreateQuery().FirstOrDefaultAsync(user => user.TelegramId.Equals(userTgId)))?.Id;
    }

    public async Task<IEnumerable<AppUser>> GetRequirementUsers(Guid requirementId)
    {
        var users = await CreateQuery()
            .Include(user => user.RequirementUsers)
            .ToListAsync();
        
        var usersInRequirement = users.FindAll(user =>
            users.SelectMany(x => x.RequirementUsers!).Select(x => x.UserId).Contains(user.Id));

        return Mapper.Map(usersInRequirement);
    }
}