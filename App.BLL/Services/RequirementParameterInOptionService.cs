using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RequirementParameterInOptionService : BaseEntityService<RequirementParameterInOption, DAL.DTO.RequirementParameterInOption, IRequirementParameterInOptionRepository>, IRequirementParameterInOptionService
{
    public RequirementParameterInOptionService(IRequirementParameterInOptionRepository repository, IMapper<RequirementParameterInOption, DAL.DTO.RequirementParameterInOption> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Guid>> GetOptionParametersId(Guid optionId)
    {
        return await Repository.GetOptionParametersId(optionId);
    }

    public async Task SetParameters(Guid optionId, IEnumerable<RequirementParameterInOption> parameterInOptions, Guid userId)
    {
        await Repository.SetParameters(optionId, Mapper.Map(parameterInOptions), userId);
    }
}