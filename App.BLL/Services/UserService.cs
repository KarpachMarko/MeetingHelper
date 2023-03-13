using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserService : BaseEntityService<AppUser, DAL.DTO.Identity.AppUser, IUserRepository>, IUserService
{
    public UserService(IUserRepository repository, IMapper<AppUser, DAL.DTO.Identity.AppUser> mapper) : base(repository, mapper)
    {
    }

    public async Task<Guid?> GetByTgId(string userTgId)
    {
        return await Repository.GetByTgId(userTgId);
    }

    public async Task<IEnumerable<AppUser>> GetRequirementUsers(Guid requirementId)
    {
        return Mapper.Map(await Repository.GetRequirementUsers(requirementId));
    }
}