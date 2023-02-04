using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RequirementParameterService : BaseEntityUserDependentService<RequirementParameter, DAL.DTO.RequirementParameter, IRequirementParameterRepository>, IRequirementParameterService
{
    public RequirementParameterService(IRequirementParameterRepository repository, IMapper<RequirementParameter, DAL.DTO.RequirementParameter> mapper) : base(repository, mapper)
    {
    }
}