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
}