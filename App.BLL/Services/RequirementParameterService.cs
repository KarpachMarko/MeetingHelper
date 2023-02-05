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

    public async Task<IEnumerable<RequirementParameter>> GetRequirementParameters(Guid requirementId, Guid userId)
    {
        return Mapper.Map(await Repository.GetRequirementParameters(requirementId, userId));
    }

    public async Task SetRequirementParameters(Guid requirementId, IEnumerable<RequirementParameter> parameters, Guid userId)
    {
        await Repository.SetRequirementParameters(requirementId, Mapper.Map(parameters), userId);
    }
}