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
}