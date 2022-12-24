using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RequirementUserService :
    BaseEntityUserService<RequirementUser, DAL.DTO.RequirementUser, AppUser, DAL.DTO.Identity.AppUser, IRequirementUserRepository>,
    IRequirementUserService
{
    public RequirementUserService(IRequirementUserRepository repository, IMapper<RequirementUser, DAL.DTO.RequirementUser> mapper) : base(repository, mapper)
    {
    }
}