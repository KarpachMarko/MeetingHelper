using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RequirementOptionService :
    BaseEntityService<RequirementOption, DAL.DTO.RequirementOption, IRequirementOptionRepository>, IRequirementOptionService

{
    public RequirementOptionService(IRequirementOptionRepository repository, IMapper<RequirementOption, DAL.DTO.RequirementOption> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<RequirementOption>> GetRequirementOptions(Guid requirementId)
    {
        return Mapper.Map(await Repository.GetRequirementOptions(requirementId));
    }

    public async Task<RequirementOption?> GetSelected(Guid requirementId)
    {
        return Mapper.Map(await Repository.GetSelected(requirementId));
    }
}