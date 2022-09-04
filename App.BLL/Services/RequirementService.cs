using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RequirementService : BaseEntityUserDependentService<Requirement, DAL.DTO.Requirement, IRequirementRepository>,
    IRequirementService
{
    public RequirementService(IRequirementRepository repository, IMapper<Requirement, DAL.DTO.Requirement> mapper) : base(repository, mapper)
    {
    }
}